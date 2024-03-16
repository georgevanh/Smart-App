using Microsoft.EntityFrameworkCore;
using SmartExercise.Server.Models;

namespace SmartExercise.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerDto> Customers { get; set; }
    }
}
