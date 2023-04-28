namespace fark_t_backend.Dto;

public class GetUserDto
{
    public Guid ID { get; set; }
    public string Username { get; set; }
    public string Fname { get; set; }
    public string Lname { get; set; }
    public string Phone { get; set; }
    public int Coin { get; set; }
}