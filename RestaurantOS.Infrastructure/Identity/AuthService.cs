using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestaurantOS.Application.DTOs.Auth;
using RestaurantOS.Application.Features.Auth.Commands;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Domain.Entities;
using RestaurantOS.Infrastructure.Persistence;

namespace RestaurantOS.Infrastructure.Identity;

public sealed class AuthService(
    UserManager<ApplicationUser> users,
    RestaurantDbContext db,
    IConfiguration configuration) : IAuthService
{
    private static readonly TimeSpan AccessTokenLifetime = TimeSpan.FromMinutes(15);
    private static readonly TimeSpan RefreshTokenLifetime = TimeSpan.FromDays(7);

    public async Task<AuthOperationResult> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken)
    {
        const string initialRole = "Waiter";
        await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        var employee = new Employee(command.FirstName, command.LastName, initialRole);
        var user = new ApplicationUser
        {
            UserName = command.Email,
            Email = command.Email,
            EmployeeId = employee.Id
        };

        var createResult = await users.CreateAsync(user, command.Password);
        if (!createResult.Succeeded)
            return AuthOperationResult.Failure(createResult.Errors.Select(error => error.Description));

        db.Employees.Add(employee);
        await db.SaveChangesAsync(cancellationToken);

        var roleResult = await users.AddToRoleAsync(user, initialRole);
        if (!roleResult.Succeeded)
            return AuthOperationResult.Failure(roleResult.Errors.Select(error => error.Description));

        await transaction.CommitAsync(cancellationToken);
        return AuthOperationResult.Success();
    }

    public async Task<AuthTokenResponse?> LoginAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await users.FindByEmailAsync(command.Email);
        if (user is null || !await users.CheckPasswordAsync(user, command.Password))
            return null;

        var refreshTokenValue = CreateRefreshTokenValue();
        var refreshTokenExpiresAtUtc = DateTime.UtcNow.Add(RefreshTokenLifetime);
        db.RefreshTokens.Add(new RefreshToken(
            user.Id,
            Guid.NewGuid(),
            HashToken(refreshTokenValue),
            refreshTokenExpiresAtUtc));
        await db.SaveChangesAsync(cancellationToken);

        return await CreateTokenResponseAsync(user, refreshTokenValue, refreshTokenExpiresAtUtc);
    }

    public async Task<AuthTokenResponse?> RefreshAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var current = await db.RefreshTokens.SingleOrDefaultAsync(
            token => token.TokenHash == HashToken(refreshToken), cancellationToken);

        if (current is null)
            return null;

        if (!current.IsActive(now))
        {
            if (current.ReplacedByTokenHash is not null)
            {
                var activeFamilyTokens = await db.RefreshTokens
                    .Where(token => token.FamilyId == current.FamilyId && token.RevokedAtUtc == null && token.ExpiresAtUtc > now)
                    .ToListAsync(cancellationToken);
                foreach (var activeToken in activeFamilyTokens)
                    activeToken.Revoke(now);
                try
                {
                    await db.SaveChangesAsync(cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    // A concurrent rotation already changed one or more tokens in this family.
                }
            }

            return null;
        }

        var user = await users.FindByIdAsync(current.UserId);
        if (user is null)
            return null;

        var nextValue = CreateRefreshTokenValue();
        var nextHash = HashToken(nextValue);
        var nextExpiresAtUtc = now.Add(RefreshTokenLifetime);
        var next = new RefreshToken(user.Id, current.FamilyId, nextHash, nextExpiresAtUtc);

        await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);
        current.Revoke(now, nextHash);
        db.RefreshTokens.Add(next);

        try
        {
            await db.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            // A token can only be rotated once, even when two requests race.
            return null;
        }

        return await CreateTokenResponseAsync(user, nextValue, nextExpiresAtUtc);
    }

    public async Task RevokeAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var token = await db.RefreshTokens.SingleOrDefaultAsync(
            item => item.TokenHash == HashToken(refreshToken), cancellationToken);

        if (token is null || !token.IsActive(now))
            return;

        token.Revoke(now);
        try
        {
            await db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            // Another request already revoked or rotated this token.
        }
    }

    private async Task<AuthTokenResponse> CreateTokenResponseAsync(
        ApplicationUser user,
        string refreshTokenValue,
        DateTime refreshTokenExpiresAtUtc)
    {
        var roles = await users.GetRolesAsync(user);
        var now = DateTime.UtcNow;
        var accessExpiresAtUtc = now.Add(AccessTokenLifetime);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new(ClaimTypes.Email, user.Email!)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            notBefore: now,
            expires: accessExpiresAtUtc,
            signingCredentials: credentials);

        return new AuthTokenResponse(
            new JwtSecurityTokenHandler().WriteToken(token),
            refreshTokenValue,
            accessExpiresAtUtc,
            refreshTokenExpiresAtUtc);
    }

    private static string CreateRefreshTokenValue() =>
        Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');

    private static string HashToken(string token) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
}
