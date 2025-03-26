using Microsoft.EntityFrameworkCore;
using MicroStore.Services.ProductAPI.Domain.Models;

namespace MicroStore.Services.ProductAPI.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
