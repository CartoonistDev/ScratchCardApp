using Microsoft.EntityFrameworkCore;

namespace ScratchCard.Data;


public class AppDbContext : DbContext
{
    public DbSet<Models.ScratchCards> ScratchCards { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.ScratchCards>().HasIndex(c => c.Code).IsUnique();
    }
}
