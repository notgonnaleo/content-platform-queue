using MassTransit;
using Newsletter.Articles.Api.Entities;
using Newsletter.Articles.Api.Repositories;
using Newsletter.Events.Analytics;

namespace Newsletter.Articles.Api.Consumers.Analytics
{
    public class ArticleInteractionsTotal : IConsumer<ArticleInteractionsTotalEvent>
    {
        private readonly ArticleRepository _articleRepository;

        public ArticleInteractionsTotal(ArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task Consume(ConsumeContext<ArticleInteractionsTotalEvent> context)
        {
            var article = await _articleRepository.Get(context.Message.ArticleId);
            if(article is null) throw new Exception("Article does not exist");
            article.TotalDislikes = context.Message.TotalDislikes;
            article.TotalLikes = context.Message.TotalLikes;
            await _articleRepository.Update(article);
        }
    }
}
