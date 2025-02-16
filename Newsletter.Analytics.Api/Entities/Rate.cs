namespace Newsletter.Analytics.Api.Entities
{
    public class Rate
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public bool Liked { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
