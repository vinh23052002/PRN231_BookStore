using System.ComponentModel.DataAnnotations.Schema;

namespace Sever.Models
{
    public class BookAuthor
    {
        public int author_id { get; set; }
        public int book_id { get; set; }
        public int? author_order { get; set; }
        public float? royality_percentage { get; set; }

        [ForeignKey("author_id")]
        public Author Author { get; set; }
        [ForeignKey("book_id")]
        public Book Book { get; set; }
    }
}
