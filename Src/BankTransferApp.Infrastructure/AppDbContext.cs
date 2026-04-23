using Microsoft.EntityFrameworkCore;

namespace BankTransferApp.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
