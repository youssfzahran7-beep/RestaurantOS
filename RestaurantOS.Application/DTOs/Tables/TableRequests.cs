namespace RestaurantOS.Application.DTOs.Tables;

public sealed record CreateTableRequest(string Number, int Capacity);
public sealed record UpdateTableRequest(string Number, int Capacity);
