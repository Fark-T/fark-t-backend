using fark_t_backend.Models;
using fark_t_backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fark_t_backend.Controllers
{
    [Route("/api")]
    [ApiController]
    public class FarkController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public FarkController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

               
        [HttpGet]
        public async Task<ActionResult<Farks>> GetAllFark()
        {
            var fark = await _dbContext.Farks.ToListAsync();
            if (fark is null)
            {
                return BadRequest();
            }
            return Ok(fark);
        }

        [HttpGet("fark/{id}")]
        public async Task<ActionResult<Farks>> GetFarkbyID(Guid id)
        {
            var fark = await _dbContext.Farks.Include(f=>f.User).Include(f=>f.Order).FirstOrDefaultAsync(farks => farks.Fark_Id == id);
            if(fark is null){
                return BadRequest();
            }
            return fark;    
        }

        [HttpGet("fark/myfark/{userId}")]
        public async Task<ActionResult<Farks>> GetFarkbyuserID(int userId)
        {
            var userFarkModels = Farks.Where(f => f.User.Id == userId);
            if (userFarkModels is null)
                return NotFound();
            return Ok(userFarkModels);
        }

        [HttpPost("fark/create")]
        public async Task<ActionResult<List<Farks>>> AddFark(Farks fark)
        {

            _dbContext.Farks.Add(fark);

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("fark/status/{id}")]
        public async Task<ActionResult<List<Farks>>> UpdateFark(Guid id, string status)
        {
            var fark = Farks.Find(x => x.Fark_Id == id);
            if (fark is null)
                return NotFound();
            fark.status = status;
            return Ok(fark);
        }

        [HttpDelete("fark/{id}")]
        public async Task<ActionResult<List<Farks>>> DeleteFark(Guid id)
        {
            var fark = await _dbContext.Farks.FirstOrDefaultAsync(x => x.Fark_Id == id);
            if (fark is null)
                return NotFound();

            _dbContext.Farks.Remove(fark);

            await _dbContext.SaveChangesAsync();
            return Ok(fark);
        }
    }
}
