namespace Domain.Models;

public class Product
{
    public Guid Id { get; set; }
    
    public string Description { get; set; }

    public int Stock { get; set; }

    public Product(Guid id, string description, int stock)
    {
        Id = id;
        Description = description;
        Stock = stock;
    }
}
