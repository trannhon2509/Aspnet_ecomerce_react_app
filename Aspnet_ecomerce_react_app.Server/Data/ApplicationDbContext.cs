using Aspnet_ecomerce_react_app.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspnet_ecomerce_react_app.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
