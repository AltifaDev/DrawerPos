// File: Controllers/OrdersController.cs

using Microsoft.AspNetCore.Mvc;
using DrawerPos.Shared;
using Microsoft.EntityFrameworkCore;
using DrawerPos.Data;

namespace DrawerPos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AltifaDbContext _context;

        public OrdersController(AltifaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null || order.OrderItems == null || !order.OrderItems.Any())
            {
                return BadRequest("Order is null or does not contain any items.");
            }

            // Set the order date to current date
            order.OrderDate = DateTime.Now;

            // Add the order to the context
            await _context.Orders.AddAsync(order);

            // Add related order items
            foreach (var item in order.OrderItems)
            {
                item.Order = order;
                await _context.OrderItems.AddAsync(item);
            }

            // Add related payments if any
            if (order.Payments != null)
            {
                foreach (var payment in order.Payments)
                {
                    payment.Order = order;
                    await _context.Payments.AddAsync(payment);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(order);
        }
    }
}
