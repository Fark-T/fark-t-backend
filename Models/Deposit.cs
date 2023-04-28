using System.ComponentModel.DataAnnotations;

namespace fark_t_backend.Models
{
    public class Deposit
    {
        [Key]
        public Guid ID { get; set; }
        public string Menu { get; set; }
        public string Location { get; set; }

        public bool Status { get; set; } = false;
        // Navigation properties
        public Users User { get; set; } = null!;
        public Orders Order { get; set; } = null!;
    }
}