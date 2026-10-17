namespace RestaurantOS.Domain.Entities;

public class MenuItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    private MenuItem()
    {
        Name = string.Empty;
        Category = null!;
    }

    public MenuItem(string name, string? description, decimal price, Category category)
    {
        Validate(name, price, category);
        Name = name.Trim();
        Description = description?.Trim();
        Price = price;
        Category = category;
        CategoryId = category.Id;
    }

    public void UpdateDetails(string name, string? description, decimal price, Category category)
    {
        Validate(name, price, category);
        Name = name.Trim();
        Description = description?.Trim();
        Price = price;
        Category = category;
        CategoryId = category.Id;
    }

    public void SetAvailability(bool isAvailable) => IsAvailable = isAvailable;

    private static void Validate(string name, decimal price, Category category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(category);
        if (price < 0) throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
    }
}
