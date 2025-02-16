namespace Newsletter.Events.Analytics
{
    public class ArticleInteractionsTotalEvent
    {
        public int ArticleId { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
