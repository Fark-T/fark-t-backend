using System.ComponentModel.DataAnnotations;

namespace fark_t_backend.Models
{
    public class Users
    {
        public Users()
        {
            // Constructor logic here (if any)
        }

        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Phone { get; set; }
        public int Coin { get; set; }
    }
}