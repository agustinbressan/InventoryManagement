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

    public async Task<Product> GetByIdAsync(Guid id) => await dbContext.Products.FindAsync(id);

    public async Task<Product> CreateAsync(string description, int stock)
    {
        var newProduct = new Product(Guid.NewGuid(), description, stock);
        await dbContext.Products.AddAsync(newProduct);

        return newProduct;
    } 


    public void Delete(Product product) => dbContext.Products.Remove(product);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await dbContext.SaveChangesAsync(cancellationToken);
}
