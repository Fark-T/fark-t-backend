using System.ComponentModel.DataAnnotations;

namespace fark_t_backend.Models
{
    public class Deposit
    {
        public Deposit()
        {
            // Constructor logic here (if any)
        }
        [Key]
        public Guid D_id { get; set; }
        public string Menu { get; set; }
        public string Location { get; set; }

        // Navigation properties
        public Users Address { get; set; }
        public Orders Order { get; set; }
    }
}