using fark_t_backend.Models;
using fark_t_backend.Data;
using fark_t_backend.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace fark_t_backend.Controllers
{
    [Route("/api")]
    [ApiController]
    public class FarkController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public FarkController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("fark/{id}")]
        public async Task<ActionResult<GetDepositDto>> GetFarkbyID(Guid id)
        {
            var fark = await _dbContext.Deposits
                .Include(f => f.Order)
                .Include(f => f.User)
                .Include(f => f.Order.User)
                .FirstOrDefaultAsync(Deposit => Deposit.ID == id);
            if (fark is null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<GetDepositDto>(fark));
        }

        [HttpGet("fark/myfark/{userId}")]
        public async Task<ActionResult<List<GetDepositDto>>> GetFarkbyuserID(Guid userId)
        {
            var userFarkModels = await _dbContext.Deposits
                .Include(f => f.User)
                .Include(f => f.Order)
                .Include(f => f.Order.User)
                .Where(f => f.User.ID == userId).ToListAsync();
            if (userFarkModels is null)
                return NotFound();
  
            return Ok(userFarkModels.Select(user => _mapper.Map<GetDepositDto>(user)).ToList());
        }

        [HttpPost("fark/create")]
        public async Task<ActionResult<GetDepositDto>> AddFark(CreateDepositDto request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.ID == request.UserID);
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.ID == request.OrderID);
            if (user is null || order is null)
            {
                return NotFound();
            }

            if (user.Coin <= 0)
            {
                return BadRequest("coin doesn't enough");
            }

            if (order.Count >= order.Limit)
            {
                return BadRequest("limit order");
            }

            user.Coin -= 1;
            order.Count += 1;

            var newFark = _mapper.Map<Deposit>(request);
            newFark.ID = Guid.NewGuid();
            newFark.User = user;
            newFark.Order = order;


            _dbContext.Deposits.Add(newFark);

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("fark/status/{id}")]
        public async Task<ActionResult<List<Deposit>>> UpdateFark(Guid id)
        {
            var fark = await _dbContext.Deposits
                .Include(f => f.User)
                .Include(f => f.Order)
                .FirstOrDefaultAsync(f => f.ID == id);

            if (fark is null)
                return NotFound();

            fark.Status = !fark.Status;

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("fark/{id}")]
        public async Task<ActionResult> DeleteFark(Guid id)
        {
            var fark = await _dbContext.Deposits
                .Include(f => f.User)
                .Include(f => f.Order)
                .FirstOrDefaultAsync(Deposit => Deposit.ID == id);

            if (fark is null) return NotFound();

            if (fark.Status == true) return BadRequest();

            fark.Order.Count -= 1;
            fark.User.Coin += 1;

            _dbContext.Deposits.Remove(fark);

            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
