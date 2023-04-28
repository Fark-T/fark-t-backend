namespace fark_t_backend.Dto
{
    public class GetDepositDto
    {
        public Guid ID { get; set; }
        public string Menu { get; set; }
        public string Location { get; set; }
        public bool Status { get; set; } = true;

        public GetUserDto User { get; set; } = null!;
        public GetOrdersDto Order { get; set; } = null!;
    }
}
