using System.Security.Claims;
using AutoMapper;
using fark_t_backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fark_t_backend.Models;
using fark_t_backend.Dto;
using fark_t_backend.Provider;
using Microsoft.AspNetCore.Authorization;

namespace fark_t_backend.Controllers;

[ApiController]
[Route("/api")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IHttpContextProvider _contextProvider;

    public OrdersController(ILogger<OrdersController> logger, AppDbContext dbContext, IMapper mapper, IHttpContextProvider contextProvider)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;
        _contextProvider = contextProvider;
    }

    [AllowAnonymous]
    [HttpGet("order")] 
    public async Task<ActionResult<List<GetOrdersDto>>> GetOrders()
    {
        var id = _contextProvider.GetCurrentUser();
        var orders = await _dbContext.Orders
            .Where(o => o.User.ID != id && o.Status == true)
            .Include(o => o.User)
            .ToListAsync();
        if (!orders.Any())
        {
            return NotFound();
        }
        return orders.Select(o =>_mapper.Map<GetOrdersDto>(o)).ToList();
    }

    [HttpGet("order/{id}")]
    public async Task<ActionResult<GetOrdersDto?>> GetOrder(Guid id)
    {
        var order = await _dbContext.Orders
            .Include(o => o.User)
            .FirstOrDefaultAsync(orders => orders.ID == id);
        if(order == null){
            return NotFound();
        }
        return _mapper.Map<GetOrdersDto>(order);
    }

    [HttpGet("myorder/{id}")]
    public async Task<ActionResult<List<GetOrdersDto>>> GetMyOrder(Guid id)
    {
        var myOrder = await _dbContext.Orders
            .Where(o => o.User.ID == id && o.Status == true)
            .Include(o => o.User)
            .ToListAsync();

        if (!myOrder.Any())
        {
            return NotFound();
        }

        return myOrder.Select(o =>_mapper.Map<GetOrdersDto>(o)).ToList();;
    }
    
    [HttpDelete("order/delete/{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var order = await _dbContext.Orders.FindAsync(id);
        var depositList = await _dbContext.Deposits
            .Where(d => d.ID == id)
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
    public async Task<ActionResult<GetOrdersDto>> CreateOrder(OrdersDto request)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync( u=> u.ID == request.UserID);
        if(user is null){
            return NotFound();
        }

        var newOrder = _mapper.Map<Orders>(request);
        newOrder.ID = Guid.NewGuid();
        newOrder.User = user;

        _dbContext.Orders.Add(newOrder);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction("GetOrder", new { id = newOrder.ID }, _mapper.Map<GetOrdersDto>(newOrder)); 
    }

    [HttpPut("order/update/status")]
    public async Task<ActionResult<UpdateStatusOrderDto>> UpdateStatus(UpdateStatusOrderDto request)
    {
        var order = await _dbContext.Orders
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.ID == request.ID);

        if (order == null)
        {
            return NotFound();
        }

        _mapper.Map(request, order);

        if (order.Status == false)
        {
            if (order.User != null)
            {
                order.User.Coin += Int32.Abs(order.Count);
            }
            else
            {
                return NotFound();
            }
        }

        await _dbContext.SaveChangesAsync();

        return Ok();
    }

}