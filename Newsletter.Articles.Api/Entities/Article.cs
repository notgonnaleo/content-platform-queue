namespace Newsletter.Articles.Api.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Header { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}
