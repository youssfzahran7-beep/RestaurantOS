namespace RestaurantOS.Domain.Entities;
using RestaurantOS.Domain.Enums;

public class RestaurantTable
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Number { get; private set; }
    public int Capacity { get; private set; }
    public TableStatus Status { get; private set; } = TableStatus.Available;
    public ICollection<Order> Orders { get; private set; } = new List<Order>();

    private RestaurantTable() => Number = string.Empty;

    public RestaurantTable(string number, int capacity)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(number);
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
        Number = number.Trim();
        Capacity = capacity;
    }

    public void UpdateDetails(string number, int capacity)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(number);
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
        Number = number.Trim();
        Capacity = capacity;
    }

    public void ChangeStatus(TableStatus status) => Status = status;
}
