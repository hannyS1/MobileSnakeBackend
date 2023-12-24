using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MobileSnake.Entities;

namespace MobileSnake.Database;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Result> Results { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}