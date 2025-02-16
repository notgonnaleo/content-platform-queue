using MassTransit;
using Newsletter.Analytics.Api.Entities;
using Newsletter.Analytics.Api.Repositories;
using Newsletter.Events.Analytics;
using Newsletter.Events.Articles;

namespace Newsletter.Analytics.Api.Consumers.Articles
{
    public class ArticleRated : IConsumer<ArticleRatedEvent>
    {
        private readonly AnalyticRepository _analyticRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public ArticleRated(AnalyticRepository analyticRepository, IPublishEndpoint publishEndpoint)
        {
            _analyticRepository = analyticRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<ArticleRatedEvent> context)
        {
            await _analyticRepository.Create(new Rate()
            {
                ArticleId = context.Message.ArticleId,
                CreatedAt = context.Message.RatedAt,
                Liked = context.Message.IsLike
            });

            var articleAnalytics = await _analyticRepository.Get(context.Message.ArticleId);
            await _publishEndpoint.Publish(new ArticleInteractionsTotalEvent()
            {
                ArticleId = context.Message.ArticleId,
                TotalDislikes = articleAnalytics.TotalDislikes,
                TotalLikes = articleAnalytics.TotalLikes,
                CreatedAt = articleAnalytics.CreatedAt
            });
        }
    }
}
