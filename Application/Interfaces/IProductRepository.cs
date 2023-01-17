using Domain.Models;

namespace Application.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product> GetByIdAsync(Guid id);

    Task<Product> CreateAsync(string description, int stock);

    void Delete(Product product);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}