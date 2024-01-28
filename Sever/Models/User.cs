using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sever.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string email_address { get; set; }
        // at least 8 characters, at least one uppercase letter, one lowercase letter and one number
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters, at least one uppercase letter, one lowercase letter and one number.")]
        public string password { get; set; }
        public string? source { get; set; }
        public string? first_name { get; set; }
        public string? middle_name { get; set; }
        public string? last_name { get; set; }
        public int role_id { get; set; }
        public int pub_id { get; set; }
        public DateTime? hire_date { get; set; }

        [ForeignKey("role_id")]
        public Role Role { get; set; }
        [ForeignKey("pub_id")]
        public Publisher Publisher { get; set; }
    }
}
