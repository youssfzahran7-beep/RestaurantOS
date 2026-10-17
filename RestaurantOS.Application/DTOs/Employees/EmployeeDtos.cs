namespace RestaurantOS.Application.DTOs.Employees;

public sealed record EmployeeDto(Guid Id, string FirstName, string LastName, string Role);
public sealed record CreateEmployeeRequest(string FirstName, string LastName, string Role);
public sealed record UpdateEmployeeRequest(string FirstName, string LastName, string Role);
