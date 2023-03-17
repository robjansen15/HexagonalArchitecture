using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfigurations;

namespace Persistence;

public class AppDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "default";
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options){ }
    
    public DbSet<Sample> Sample { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SampleEntityTypeConfiguration());
    }
}