using EQUOR.Models;
using Microsoft.EntityFrameworkCore;

namespace EQUOR.DataContext
{
	public class DataDBContext : DbContext
	{
        public DataDBContext(DbContextOptions dbContextOptions ) :base ( dbContextOptions ) { 
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Opinions> Opinions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.Name == "Id" || p.Name == $"{entityType.ClrType.Name}Id");

                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(int))
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                            .ValueGeneratedOnAdd();
                    }
                }
            }
        }

    }
}
