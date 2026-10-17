namespace RestaurantOS.Domain.Entities;
using RestaurantOS.Domain.Enums;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid RestaurantTableId { get; private set; }
    public RestaurantTable RestaurantTable { get; private set; }
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
    public Bill? Bill { get; private set; }

    private Order() => RestaurantTable = null!;

    public Order(RestaurantTable restaurantTable)
    {
        ArgumentNullException.ThrowIfNull(restaurantTable);
        RestaurantTable = restaurantTable;
        RestaurantTableId = restaurantTable.Id;
    }

    public void AddItem(MenuItem menuItem, int quantity)
    {
        ArgumentNullException.ThrowIfNull(menuItem);
        OrderItems.Add(new OrderItem(this, menuItem, quantity));
    }

    public void ChangeStatus(OrderStatus status)
    {
        if (status == OrderStatus.Confirmed && Status != OrderStatus.Pending ||
            status == OrderStatus.Preparing && Status != OrderStatus.Confirmed ||
            status == OrderStatus.Ready && Status != OrderStatus.Preparing ||
            status == OrderStatus.Served && Status != OrderStatus.Ready ||
            status == OrderStatus.Completed && Status != OrderStatus.Served ||
            status == OrderStatus.Cancelled && Status is OrderStatus.Completed or OrderStatus.Cancelled)
            throw new InvalidOperationException($"Cannot change order status from {Status} to {status}.");
        Status = status;
    }
}
