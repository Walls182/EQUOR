using EQUOR.Models;
using Microsoft.EntityFrameworkCore;

namespace EQUOR.DataContext
{
    public class DataDBContext : DbContext
    {
        public DataDBContext()
        {
        }

        public DataDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Opinions> Opinions { get; set; }
        public DbSet<Role> Roles { get; set; } 
        public DbSet<Manager> Managers { get; set; }

      

    }
}
