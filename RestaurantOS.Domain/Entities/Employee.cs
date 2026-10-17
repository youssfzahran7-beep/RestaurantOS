namespace RestaurantOS.Domain.Entities;

public class Employee
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Role { get; private set; }

    private Employee()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Role = string.Empty;
    }

    public Employee(string firstName, string lastName, string role)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(role);
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Role = role.Trim();
    }

    public void UpdateDetails(string firstName, string lastName, string role)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(role);
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Role = role.Trim();
    }
}
