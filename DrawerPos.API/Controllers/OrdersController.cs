// File: DrawerPos.API/Controllers/OrdersController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DrawerPos.Shared;
using DrawerPos.Data;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DrawerPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AltifaDbContext _context;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(AltifaDbContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null || order.OrderItems == null || !order.OrderItems.Any())
            {
                _logger.LogWarning("Invalid order: Order is null or does not contain any items.");
                return BadRequest("Order is null or does not contain any items.");
            }

            try
            {
                // Generate BillNo (you can implement your own logic here)
                order.BillNo = '#'+ GenerateUniqueNumber().ToString();

                // Set the order date to the current date
                order.OrderDate = DateTime.Now;
 
                // Log the order details
                _logger.LogInformation("Creating order: {@Order}", order);

                // Attach the order and related entities to the context in a single batch
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating order.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        private int GenerateUniqueNumber()
        {
            Guid guid = Guid.NewGuid();
            string guidString = guid.ToString("N"); // Get GUID without dashes
            string guidSubstring = guidString.Substring(0, 8); // Extract first 8 characters
            long parsedNumber = long.Parse(guidSubstring, System.Globalization.NumberStyles.HexNumber); // Parse as hexadecimal number

            // Ensure the parsed number fits within the range of an integer
            int uniqueNumber = (int)(parsedNumber % int.MaxValue); // Use modulo to fit within int range
            return uniqueNumber;
        }

        [HttpGet("last-billno")]
        public async Task<IActionResult> GetLastBillNo()
        {
            try
            {
                var lastOrder = await _context.Orders
                    .OrderByDescending(o => o.OrderDate)
                    .FirstOrDefaultAsync();

                if (lastOrder == null)
                {
                    return NotFound("No orders found.");
                }

                return Ok(lastOrder.BillNo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching last BillNo.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
