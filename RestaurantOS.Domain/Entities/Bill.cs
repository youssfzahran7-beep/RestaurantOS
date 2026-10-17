namespace RestaurantOS.Domain.Entities;
using RestaurantOS.Domain.Enums;

public class Bill
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid OrderId { get; private set; }
    public Order Order { get; private set; }
    public decimal Subtotal { get; private set; }
    public decimal Tax { get; private set; }
    public decimal Total { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.Pending;

    private Bill() => Order = null!;

    public Bill(Order order, decimal subtotal, decimal tax, decimal discount = 0)
    {
        ArgumentNullException.ThrowIfNull(order);
        if (subtotal < 0 || tax < 0 || discount < 0 || discount > subtotal + tax) throw new ArgumentOutOfRangeException(nameof(subtotal));
        Order = order;
        OrderId = order.Id;
        Subtotal = subtotal;
        Tax = tax;
        Total = subtotal + tax - discount;
    }

    public void MarkPaid() => PaymentStatus = PaymentStatus.Paid;
}
