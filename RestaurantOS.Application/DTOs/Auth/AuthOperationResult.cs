namespace RestaurantOS.Application.DTOs.Auth;

public sealed record AuthOperationResult(bool Succeeded, IReadOnlyList<string> Errors)
{
    public static AuthOperationResult Success() => new(true, []);
    public static AuthOperationResult Failure(IEnumerable<string> errors) => new(false, errors.ToArray());
}
