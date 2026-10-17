using Microsoft.AspNetCore.Identity;

namespace RestaurantOS.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser
{
    public Guid? EmployeeId { get; set; }
}
