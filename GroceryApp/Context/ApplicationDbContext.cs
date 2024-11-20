using GroceryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryApp.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base (contextOptions)
        { 

        }

        public DbSet<Items> Item { get; set; }

        public DbSet<Categories> Category { get; set; }
    }
}
