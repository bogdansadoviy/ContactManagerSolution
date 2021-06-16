using ContactManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Record> Records { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
