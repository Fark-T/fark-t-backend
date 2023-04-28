using fark_t_backend.Dto;
using fark_t_backend.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace fark_t_backend.Services
{
    public interface IAuthService
    {
        Task<Result<Users>> Register(CreateUserDto request);
        Task<Result<TokenDto>> Login(LoginDto request);
    }
}
