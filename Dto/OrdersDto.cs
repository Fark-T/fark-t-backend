using fark_t_backend.Models;
namespace fark_t_backend.Dto;

public class OrdersDto
{
    public string Restaurant { get; set; }
    public string Category { get; set; }
    public int Limit { get; set; }
    public Guid UserID { get; set; } 
}
