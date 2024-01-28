using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sever.Models
{
    public class Book
    {
        [Key]
        public int book_id { get; set; }
        public string title { get; set; }
        public string? type { get; set; }
        public int pub_id { get; set; }
        public float? price { get; set; }
        public string? advance { get; set; }
        public float? royalty { get; set; }
        public float? ytd_sales { get; set; }
        public string? notes { get; set; }
        public DateTime? publisher_date { get; set; }
        [ForeignKey("pub_id")]
        public Publisher Publisher { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
