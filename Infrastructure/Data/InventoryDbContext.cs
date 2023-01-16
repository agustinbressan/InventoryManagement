using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class InventoryDbContext : DbContext {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Using the EF Core In-Memory Db to simplify the persistence implementation
        optionsBuilder.UseInMemoryDatabase(databaseName: "InventoryManagementDb");
    }

    public DbSet<Product> Products { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}