using Domain.Models;

namespace Application.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(Guid id);

    Task CreateAsync(string description, int stock);

    Task UpdateAsync(Guid id, string description, int stock);

    Task DeleteAsync(Guid guid);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}