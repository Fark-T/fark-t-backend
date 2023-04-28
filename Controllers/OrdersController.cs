using fark_t_backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fark_t_backend.Models;
using fark_t_backend.RequestModel;


namespace fark_t_backend.Controllers;

[ApiController]
[Route("/api")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly AppDbContext _dbContext;

    public OrdersController(ILogger<OrdersController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    [HttpGet("order")]
    public async Task<ActionResult<List<Orders>>> GetOrders([FromQuery(Name = "id")] Guid id)
    {
        var orders = await _dbContext.Orders
            .Where(o => o.User.Id != id)
            .Include(o => o.User)
            .Select(o=>new Orders
            {
                Order_Id = o.Order_Id,
                Restaurant = o.Restaurant,
                Category = o.Category,
                Limit = o.Limit,
                Count = o.Count,
                User = new Users
                {
                    Id = o.User.Id,
                    Username = o.User.Username,
                    Fname = o.User.Fname,
                    Lname = o.User.Lname,
                    Phone = o.User.Phone,
                    Coin = o.User.Coin
                }
                
            })
            .ToListAsync();
        if (!orders.Any()) return NotFound();
        return orders;
    }

    [HttpGet("order/{id}")]
    public async Task<ActionResult<Orders?>> GetOrder([FromQuery(Name = "id")] Guid id)
    {
        var order = await _dbContext.Orders
            .Include(o => o.User)
            .Select(o=>new Orders
            {
                Order_Id = o.Order_Id,
                Restaurant = o.Restaurant,
                Category = o.Category,
                Limit = o.Limit,
                Count = o.Count,
                User = new Users
                {
                    Id = o.User.Id,
                    Username = o.User.Username,
                    Fname = o.User.Fname,
                    Lname = o.User.Lname,
                    Phone = o.User.Phone,
                    Coin = o.User.Coin
                }
                
            })
            .FirstOrDefaultAsync(orders => orders.Order_Id == id);
        if(order == null){
            return NotFound();
        }
        return order;
    }

    [HttpGet("myorder")]
    public async Task<ActionResult<List<Orders>>> GetMyOrder([FromQuery(Name = "id")] Guid id)
    {
        var myOrder = await _dbContext.Orders
            .Where(o => o.User.Id == id)
            .Include(o => o.User)
            .Select(o=>new Orders
            {
                Order_Id = o.Order_Id,
                Restaurant = o.Restaurant,
                Category = o.Category,
                Limit = o.Limit,
                Count = o.Count,
                User = new Users
                {
                    Id = o.User.Id,
                    Username = o.User.Username,
                    Fname = o.User.Fname,
                    Lname = o.User.Lname,
                    Phone = o.User.Phone,
                    Coin = o.User.Coin
                }
                
            })
            .ToListAsync();

        if (!myOrder.Any())
        {
            return NotFound();
        }

        return myOrder;
    }
    
    [HttpDelete("order/delete/{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var order = await _dbContext.Orders.FindAsync(id);
        var depositList = await _dbContext.Deposits
            .Where(d => d.D_id == id)
            .Include(d => d.Order)
            .ToListAsync();
        if (order == null)
        {
            return NotFound();
        }
        _dbContext.Deposits.RemoveRange(depositList);
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpPost("order/create")]
    public async Task<ActionResult<Orders>> CreateOrder(CreateOrderRequest order)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync( u=> u.Id == order.User_Id);
        if(user is null){
            return NotFound();
        }

        var newOrder = new Orders
        {
            Restaurant = order.Restaurant,
            Category = order.Category,
            Limit = order.Limit,
            Count = order.Count,
            Status = order.Status,
            User = user
        };
        _dbContext.Orders.Add(newOrder);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction("GetOrder", new { id = newOrder.Order_Id }, order); 
    }

    // [HttpPost("order/create")]
    // public async Task<ActionResult<Orders>> CreateOrderAsync(Orders order, Guid userId)
    // {
    //     // Retrieve the user associated with the given userId
    //     Users user = await _dbContext.Users.FindAsync(userId);
    //     if (user == null)
    //     {
    //         return NotFound();
    //     }
    //     order.User = user;
    //     order.Order_Id = Guid.NewGuid();
    //     
    //     _dbContext.Orders.Add(order);
    //     await _dbContext.SaveChangesAsync();
    //     
    //     return CreatedAtAction("GetOrder", new { id = order.Order_Id }, order);
    // }
}