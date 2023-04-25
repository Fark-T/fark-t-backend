using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace fark_t_backend.Models
{
    public class Fark
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid fark_id { get; set; }
        public string menu { get; set; }
        public string location { get; set; }
        public string status { get; set; }

        public Users User { get; set; }
        public Orders Order { get; set; }
    }
}
