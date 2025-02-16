using System.Text.Json.Serialization;

namespace Newsletter.Articles.Api.Entities
{
    public class ArticleEvent
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public ArticleEventType EventType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
