public class Cart
{
    public required int Id { get; set; }

    // A cart can have many items
    public List<CartItem> Items { get; set; } = new();
}