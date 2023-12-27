namespace Leo.Data.Domain.Entities;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid AccountId { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
