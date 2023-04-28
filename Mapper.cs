using AutoMapper;
using fark_t_backend.Models;
using fark_t_backend.Dto;

namespace fark_t_backend;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Orders,GetOrdersDto>();
        CreateMap<OrdersDto, Orders>();      
        CreateMap<UpdateStatusOrderDto,Orders>();   
        CreateMap<Users,GetUserDto>();
        CreateMap<CreateUserDto, Users>();
        CreateMap<UpdateUserDto, Users>();
        CreateMap<Deposit, GetDepositDto>();
        CreateMap<CreateDepositDto, Deposit>();
    }
}
