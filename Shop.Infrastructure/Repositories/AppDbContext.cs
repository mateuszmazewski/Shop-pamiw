using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;

namespace Shop.Infrastructure.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // Dodanie tabel do bazy danych
        public DbSet<Customer> Customer { get; set; }
    }
}
