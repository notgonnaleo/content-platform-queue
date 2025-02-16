using Microsoft.EntityFrameworkCore;
using Newsletter.Articles.Api.Entities;

namespace Newsletter.Articles.Api.Contexts
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

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleEvent> ArticleEvents { get; set; }
    }
}
