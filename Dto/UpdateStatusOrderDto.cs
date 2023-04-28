namespace fark_t_backend.Dto;

public class UpdateStatusOrderDto
{
    public Guid ID { get; set; }
    public bool Status { get; set; }
    public Guid UserID { get; set; } 
}