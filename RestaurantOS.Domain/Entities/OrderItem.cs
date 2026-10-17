namespace RestaurantOS.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid OrderId { get; private set; }
    public Order Order { get; private set; }
    public Guid MenuItemId { get; private set; }
    public MenuItem MenuItem { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    private OrderItem()
    {
        Order = null!;
        MenuItem = null!;
    }

    public OrderItem(Order order, MenuItem menuItem, int quantity)
    {
        ArgumentNullException.ThrowIfNull(order);
        ArgumentNullException.ThrowIfNull(menuItem);
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        Order = order;
        OrderId = order.Id;
        MenuItem = menuItem;
        MenuItemId = menuItem.Id;
        Quantity = quantity;
        UnitPrice = menuItem.Price;
    }
}
