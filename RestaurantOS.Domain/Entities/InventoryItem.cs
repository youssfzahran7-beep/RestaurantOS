namespace RestaurantOS.Domain.Entities;

public class InventoryItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public decimal Quantity { get; private set; }
    public string Unit { get; private set; }

    private InventoryItem()
    {
        Name = string.Empty;
        Unit = string.Empty;
    }

    public InventoryItem(string name, decimal quantity, string unit)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(unit);
        if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        Name = name.Trim();
        Quantity = quantity;
        Unit = unit.Trim();
    }
}
