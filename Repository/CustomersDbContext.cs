using Microsoft.EntityFrameworkCore;
using Angular_ASPNETCore_CustomersService.Models;

namespace Angular_ASPNETCore_CustomersService.Repository
{
    public class CustomersDbContext : DbContext
    {
        public DbSet<Entries> Customers { get; set; }
        public DbSet<User> Users { get; set; }

        public CustomersDbContext (DbContextOptions<CustomersDbContext> options) : base(options) { }
    }
}
