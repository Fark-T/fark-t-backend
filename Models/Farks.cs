using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace fark_t_backend.Models
{
    public class Farks
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Fark_Id { get; set; }
        public string menu { get; set; }
        public string location { get; set; }
        public string status { get; set; }

        public Users User { get; set; }
        public Orders Order { get; set; }
    }
}
