using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    public InventoryDbContext dbContext;

    public ProductRepository(InventoryDbContext inventoryDbContext)
    {
        dbContext = inventoryDbContext;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() => await dbContext.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(Guid id) => await dbContext.Products.FindAsync(id);

    public async Task CreateAsync(string description, int stock) => await dbContext.Products.AddAsync(new Product(Guid.NewGuid(), description, stock));

    public async Task UpdateAsync(Guid id, string description, int stock)
    {
        var product = await GetProductAsync(id);

        product.Description = description;
        product.Stock = stock;
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await GetProductAsync(id);

        dbContext.Products.Remove(product);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await dbContext.SaveChangesAsync(cancellationToken);

    private async Task<Product> GetProductAsync(Guid id)
    {
        var product = await GetByIdAsync(id);

        if (product == null) throw new KeyNotFoundException("Product not found.");

        return product;
    }
}
