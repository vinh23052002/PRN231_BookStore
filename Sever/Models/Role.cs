using System.ComponentModel.DataAnnotations;

namespace Sever.Models
{
    public class Role
    {
        [Key]
        public int role_id { get; set; }
        public int role_desc { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
