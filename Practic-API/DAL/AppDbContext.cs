using Microsoft.EntityFrameworkCore;
using Practic_API.Entities;

namespace Practic_API.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category>Categories { get; set; }
    }
}
