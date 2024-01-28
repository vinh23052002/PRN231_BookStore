using System.ComponentModel.DataAnnotations;

namespace Sever.Models
{
    public class Publisher
    {
        [Key]
        public int pub_id { get; set; }
        public string? publisher_name { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? country { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
