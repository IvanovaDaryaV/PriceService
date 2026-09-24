using Microsoft.EntityFrameworkCore;
using PriceService.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<PriceHistory> PriceHistories { get; set; }
}