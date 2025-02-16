namespace Newsletter.Events.Articles
{
    public class ArticleRatedEvent
    {
        public int ArticleId { get; set; }
        public bool IsLike { get; set; }
        public DateTime RatedAt { get; set; }
    }
}
