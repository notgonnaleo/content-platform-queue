using Microsoft.EntityFrameworkCore;
using Newsletter.Analytics.Api.Contexts;
using Newsletter.Analytics.Api.Entities;
using Newsletter.Analytics.Api.Models;

namespace Newsletter.Analytics.Api.Repositories
{
    public class AnalyticRepository
    {
        private readonly AppDbContext _context;

        public AnalyticRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Overview> Get(int articleId)
        {
            var articleLogs = await _context.Rates
                .Where(x => x.ArticleId == articleId)
                .ToListAsync();

            if (articleLogs is null || articleLogs.Count == 0)
                throw new Exception($"Failed while fetching analytics for article: {articleId}");

            var reference = articleLogs.First();
            var totalLikes = articleLogs.Count(x => x.Liked);
            var totalDislikes = articleLogs.Count(x => !x.Liked);

            return new Overview()
            {
                ArticleId = articleId,
                TotalLikes = totalLikes,
                TotalDislikes = totalDislikes,
                CreatedAt = reference.CreatedAt
            };
        }

        public async Task<bool> Create(Rate rate)
        {
            _context.Rates.Add(rate);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
