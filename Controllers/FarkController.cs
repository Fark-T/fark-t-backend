using fark_t_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Numerics;
using System.Xml.Linq;

namespace fark_t_backend.Controllers
{
    [Route("/api")]
    [ApiController]
    public class FarkController : ControllerBase
    {
         private static List<Fark> farks = new List<Fark>
            {
                new Fark
                {
                    fark_id = Guid.NewGuid(),
                    menu = "padthai",
                    location = "tops",
                    status = "ready",
                    User = new Users()
                    {
                        Id = 3,
                        Username = "string",
                        Password = "string",
                        Fname = "string",
                        Lname = "string",
                        Phone = "string",
                        Coin = 0

                    },
                    Order = new Orders()
                    {
                        Restaurant = "string",
                        Category = "string",
                        Limit = 15,
                        Count = 0
                    }
                },
                new Fark
                {
                    fark_id = Guid.NewGuid(),
                    menu = "kaopad",
                    location = "billion",
                    status = "not ready",
                    User = new Users()
                    {
                        Id = 2,
                        Username = "string",
                        Password = "string",
                        Fname = "string",
                        Lname = "string",
                        Phone = "string",
                        Coin = 0

                    },
                    Order = new Orders()
                    {
                        Restaurant = "string",
                        Category = "string",
                        Limit = 15,
                        Count = 0
                    }
                },
                new Fark
                {
                    fark_id = Guid.NewGuid(),
                    menu = "cake",
                    location = "cafe",
                    status = "ready",
                    User = new Users()
                    {
                        Id = 2,
                        Username = "string",
                        Password = "string",
                        Fname = "string",
                        Lname = "string",
                        Phone = "string",
                        Coin = 0

                    },
                    Order = new Orders()
                    {
                        Restaurant = "string",
                        Category = "string",
                        Limit = 15,
                        Count = 0
                    }
                },
                new Fark
                {
                    fark_id = Guid.NewGuid(),
                    menu = "string",
                    location = "string",
                    status = "string",
                    User = new Users()
                    {
                        Id = 0,
                        Username = "string",
                        Password = "string",
                        Fname = "string",
                        Lname = "string",
                        Phone = "string",
                        Coin = 0

                    },
                    Order = new Orders()
                    {
                        Restaurant = "string",
                        Category = "string",
                        Limit = 15,
                        Count = 0
                    }
                }
            };

        [HttpGet]
        public async Task<ActionResult<List<Fark>>> GetAllFark()
        {
            return Ok(farks);
        }

        [HttpGet("fark/{id}")]
        public async Task<ActionResult<List<Fark>>> GetFarkbyID(Guid id)
        {
            var fark = farks.Find(x => x.fark_id == id);
            if (fark is null)
                return NotFound();
            return Ok(fark);
        }

        [HttpGet("fark/myfark/{userId}")]
        public async Task<ActionResult<List<Fark>>> GetFarkbyuserID(int userId)
        {
            var userFarkModels = farks.Where(f => f.User.Id == userId);
            if (userFarkModels is null)
                return NotFound();
            return Ok(userFarkModels);
        }
    }
}
