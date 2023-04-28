namespace fark_t_backend.Dto;

public class GetOrdersDto
{
    public Guid ID { get; set; }
    public string Restaurant { get; set; }
    public string Category { get; set; }
    public int Limit { get; set; }
    public int Count { get; set; }
    public bool Status { get; set; }

    // Navigation property
    public GetUserDto User { get; set; } 
}