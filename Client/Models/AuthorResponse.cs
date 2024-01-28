using System.ComponentModel.DataAnnotations;
namespace Client.Models
{
    public class AuthorResponse
    {
        public int author_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? city { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? email_address { get; set; }
    }
}
