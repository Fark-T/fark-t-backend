﻿using AutoMapper;
using fark_t_backend.Models;
using fark_t_backend.Dto;

namespace fark_t_backend;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Orders,GetOrdersDto>();
        CreateMap<OrdersDto, Orders>();
        CreateMap<Users,GetUserDto>();
       
    }
}