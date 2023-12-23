using Microsoft.EntityFrameworkCore;
using Preparation.Models;

namespace Preparation.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
