namespace Sever.DTO
{
    public class BookRequest
    {
        public int pub_id { get; set; }
        public string title { get; set; }
        public string? type { get; set; }
        public float? price { get; set; }
        public DateTime? publisher_date { get; set; }
        public string? notes { get; set; }

    }
}
