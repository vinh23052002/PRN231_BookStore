namespace Client.Models
{
    public class RegisterRequest
    {
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
    }
}
