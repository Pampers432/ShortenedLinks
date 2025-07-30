namespace ShortenedLinks.Models
{
    public class EntryLink
    {
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl{ get; set; }
        public DateTime CreatedDate { get; set; }
        public int JumpsCount { get; set; }
    }
}
