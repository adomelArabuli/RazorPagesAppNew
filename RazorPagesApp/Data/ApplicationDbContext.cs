using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data.Model.DbModels;

namespace PizzaShop.Data
{
	public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<PizzaOrder> PizzaOrders { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
    }
}
