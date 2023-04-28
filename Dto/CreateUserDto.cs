using System.ComponentModel.DataAnnotations;

namespace fark_t_backend.Dto
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
