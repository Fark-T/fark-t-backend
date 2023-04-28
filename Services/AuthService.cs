using AutoMapper;
using fark_t_backend.Data;
using fark_t_backend.Dto;
using fark_t_backend.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fark_t_backend.Services
{
    public class AuthService : IAuthService
    {
        public static Users user = new Users();

        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly AppDbContext _dbContext;

        public AuthService(IMapper mapper, IConfiguration config, AppDbContext dbContext)
        {
            _mapper = mapper;
            _config = config;
            _dbContext = dbContext;
        }

        public async Task<Result<TokenDto>> Login(LoginDto request)
        {
            var have_user = await _dbContext.Users.AnyAsync(u => u.Username == request.Username);
            if (!have_user) return Result.Fail(new Error("Wrong username or password.")); 
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user!.Password)) return Result.Fail(new Error("Wrong username or password."));

            string token = CreateToken(user);
            var tokenDto = new TokenDto { Token = token }; 

            return Result.Ok(tokenDto);
        }

        public async Task<Result<Users>> Register(CreateUserDto request)
        {
            try
            {
                var have_user = await _dbContext.Users.AnyAsync(u => u.Username == request.Username);
                if (have_user) return Result.Fail(new Error("User already exist."));

                var user = _mapper.Map<Users>(request);
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                user.ID = Guid.NewGuid();

                await _dbContext.Users.AddAsync(user);
                _dbContext.SaveChanges();

                return Result.Ok();
            }
            catch (Exception)
            {
                return Result.Fail(new Error("400"));
            }
        }

        private string CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim("userId", user.ID.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
