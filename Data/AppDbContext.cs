using Microsoft.EntityFrameworkCore;
using fark_t_backend.Models;

namespace fark_t_backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }

    public DbSet<Users> Users { get; set; }
    public DbSet<Deposit> Deposits { get; set; }
    public DbSet<Orders> Orders { get; set; }
  
}