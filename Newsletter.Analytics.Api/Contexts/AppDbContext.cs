using Microsoft.EntityFrameworkCore;
using Newsletter.Analytics.Api.Entities;

namespace Newsletter.Analytics.Api.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Rate> Rates { get; set; }
    }
}
