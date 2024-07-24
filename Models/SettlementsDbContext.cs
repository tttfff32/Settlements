using Microsoft.EntityFrameworkCore;
namespace Settlements.Models;
public class SettlementsDbContext : DbContext
    {
        public SettlementsDbContext(DbContextOptions<SettlementsDbContext> options) : base(options)
        { }

        public DbSet<Settlement> Settlements { get; set; } 
    }