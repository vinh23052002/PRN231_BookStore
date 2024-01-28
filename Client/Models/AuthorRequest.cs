using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class AuthorRequest
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? city { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? email_address { get; set; }
    }
}
