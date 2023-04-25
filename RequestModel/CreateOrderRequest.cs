namespace fark_t_backend.RequestModel;

public class CreateOrderRequest
{
    public Guid User_Id { get; set; }
    public string Restaurant { get; set; }
    public string Category { get; set; }
    public int Limit { get; set; }
    public int Count { get; set; }
    public string Status { get; set; }
}