using System.ComponentModel.DataAnnotations;
using fark_t_backend.Dto;

namespace fark_t_backend.Models
{
    public class Orders
    {
        public Orders()
        {
            // Constructor logic here (if any)
        }
        [Key]
        public Guid ID { get; set; }
        public string Restaurant { get; set; }
        public string Category { get; set; }
        public int Limit { get; set; }
        public int Count { get; set; }
        public bool Status { get; set; } = true;

        // Navigation property
        public Users User { get; set; } = null!;
    }
}