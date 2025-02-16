using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newsletter.Articles.Api.Contexts;
using Newsletter.Articles.Api.Entities;
using Newsletter.Events.Articles;

namespace Newsletter.Articles.Api.Repositories
{
    public class ArticleRepository
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;
        public ArticleRepository(AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<IEnumerable<Article>> Get()
        {
            var response = await _context.Articles.ToListAsync();
            return response;
        }

        public async Task<Article?> Get(int id)
        {
            var response = await _context.Articles.FindAsync(id);
            _context.ArticleEvents.Add(new ArticleEvent
            {
                ArticleId = id,
                EventType = ArticleEventType.View,
                CreatedAt = DateTime.UtcNow
            });
            return response;
        }

        public async Task<Article> Post(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<Article> Rate(int articleId, bool isLike)
        {
            var article = await _context.Articles.FindAsync(articleId);
            if (article is null) throw new Exception("Article not found");

            var newEvent = new ArticleEvent
            {
                ArticleId = articleId,
                EventType = isLike ? ArticleEventType.Like : ArticleEventType.Dislike,
                CreatedAt = DateTime.UtcNow
            };
            _context.ArticleEvents.Add(newEvent);
            await _context.SaveChangesAsync();

            await _publishEndpoint.Publish(new ArticleRatedEvent()
            {
                ArticleId = articleId,
                IsLike = isLike,
                RatedAt = newEvent.CreatedAt
            });

            // Update on runtime just so we can show the UI with the new values
            // since the the actual database change will only happen after the queue consumer processes the event
            if (isLike)
                article.TotalLikes++;
            else
                article.TotalDislikes++;

            return article;
        }

        public async Task<Article> Update(Article article)
        {
            _context.Update(article);
            await _context.SaveChangesAsync();
            return article;
        }
    }
}
