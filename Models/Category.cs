namespace LatestEcommAPI.Models;


class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public required List<Product> Products { get; set; } = new();

    public Category()
    {
        Products = new List<Product>();
    }
}