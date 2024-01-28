using System.ComponentModel.DataAnnotations;

namespace Sever.Models
{
    public class Author
    {
        [Key]
        public int author_id { get; set; }
        public string? last_name { get; set; }
        public string? first_name { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{9,10}$", ErrorMessage = "Phone number must be 9 or 10 digits.")]
        public string? phone { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? zip { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? email_address { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
