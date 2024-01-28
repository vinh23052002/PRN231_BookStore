namespace Client.Models
{
    public class BookAuthorResponse
    {
        public int author_id { get; set; }
        public int book_id { get; set; }
        public int? author_order { get; set; }
        public float? royality_percentage { get; set; }
    }
}
