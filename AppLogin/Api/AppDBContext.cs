using Microsoft.EntityFrameworkCore;
using AppLogin.Models;

namespace AppLogin.Api;

public class AppDBContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted); }
}