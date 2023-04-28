using System.ComponentModel.DataAnnotations;

namespace fark_t_backend.Models
{
    public class Users
    {
        [Key] 
        public Guid ID { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fname { get; set; } = string.Empty;
        public string Lname { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int Coin { get; set; } = 3;
    }
}