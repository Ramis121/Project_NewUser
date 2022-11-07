using Microsoft.EntityFrameworkCore;
using Project_NewUser.Models;

namespace Project_NewUser.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        public DbSet<User> users { get; set; } 
    }
}
