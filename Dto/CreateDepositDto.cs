using fark_t_backend.Models;

namespace fark_t_backend.Dto;

public class CreateDepositDto
{
    public string Menu { get; set; }
    public string Location { get; set; }

    public Guid UserID { get; set; }
    public Guid OrderID { get; set; }
}

