public class CartItem
{
    public required int Id { get; set; }
    public required int ProductId { get; set; }
    public required int Quantity { get; set; }

    // Optional: navigation property
    public Cart? Cart { get; set; }
    public int CartId { get; set; }
}