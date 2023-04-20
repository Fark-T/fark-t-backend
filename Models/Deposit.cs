using System.ComponentModel.DataAnnotations;

namespace fark_t_backend.Models
{
    public class Deposit
    {
        [Key]
        public Guid D_id { get; set; }
        public string Menu { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        // Navigation properties
        public Users User { get; set; } = null!;
        public Orders Order { get; set; } = null!;
    }
}