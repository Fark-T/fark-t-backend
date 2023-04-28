using AutoMapper;
using fark_t_backend.Data;
using fark_t_backend.Dto;
using fark_t_backend.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fark_t_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextProvider _contextProvider;
        private readonly AppDbContext _appDbContext;

        public UsersController(IMapper mapper, IHttpContextProvider contextProvider, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _contextProvider = contextProvider;
            _appDbContext = appDbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUserById(Guid id)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.ID == id);
            if (user == null) return NotFound("User doesn't exist.");
            var newUser = _mapper.Map<GetUserDto>(user);
            return Ok(newUser);
        }

        [HttpGet("current")]
        public async Task<ActionResult<GetUserDto>> GetCurrent()
        {
            var id = _contextProvider.GetCurrentUser();
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.ID == id);
            if (user == null) return Forbid();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, UpdateUserDto request)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.ID == id);
            if (user == null) return NotFound("User doesn't exist.");

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Password = request.Password;
            user.Fname = request.Fname;
            user.Lname = request.Lname;
            user.Phone = request.Phone;

            await _appDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
