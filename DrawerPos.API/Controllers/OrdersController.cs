using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DrawerPos.Shared;
using DrawerPos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
                order.BillNo = '#' + GenerateUniqueNumber().ToString();
                order.OrderDate = DateTime.Now;
                _logger.LogInformation("Creating order: {@Order}", order);

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
            string guidString = guid.ToString("N");
            string guidSubstring = guidString.Substring(0, 8);
            long parsedNumber = long.Parse(guidSubstring, System.Globalization.NumberStyles.HexNumber);
            int uniqueNumber = (int)(parsedNumber % int.MaxValue);
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .ToListAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching orders.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetDateOrders")]
        public async Task<ActionResult<IEnumerable<GroupedOrderItem>>> GetDateOrders([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                IQueryable<Order> query = _context.Orders;

                if (startDate.HasValue)
                {
                    query = query.Where(o => o.OrderDate >= startDate);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(o => o.OrderDate <= endDate);
                }

                var orders = await _context.Orders
                      .Include(o => o.OrderItems)
                      .ThenInclude(oi => oi.Product)
                      .ToListAsync();

                var groupedOrderItems = orders
     .SelectMany(o => o.OrderItems)
     .GroupBy(oi => oi.Product)  // Group by the Product object
     .Select(g => new GroupedOrderItem
     {
         Products = g.Key,  // Assign the Product object
         TotalQuantity = g.Sum(oi => oi.Quantity),
         TotalPrice = g.Sum(oi => (oi.Price ?? 0) * oi.Quantity)
     })
     .OrderBy(g => g.Products.NameDisplay)  // Sort by Product Type in ascending order
     .ToList();

                return Ok(groupedOrderItems);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("monthly-revenue")]
        public async Task<ActionResult<IEnumerable<MonthlyRevenueDto>>> GetMonthlyRevenue()
        {
            var monthlyRevenue = await _context.Orders
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(g => new MonthlyRevenueDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalAmount = g.Sum(o => o.TotalAmount ?? 0)
                })
                .OrderBy(r => r.Year).ThenBy(r => r.Month)
                .ToListAsync();

            return Ok(monthlyRevenue);
        }

        [HttpGet("weekly-monthly-revenue")]
        public async Task<ActionResult<WeeklyMonthlyRevenueDto>> GetWeeklyMonthlyRevenue()
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            var lastWeekRevenue = await _context.Orders
                .Where(o => o.OrderDate >= startOfWeek)
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new DailyRevenueDto
                {
                    Date = g.Key,
                    TotalAmount = g.Sum(o => o.TotalAmount ?? 0)
                })
                .ToListAsync();

            var thisMonthRevenue = await _context.Orders
                .Where(o => o.OrderDate >= startOfMonth)
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new DailyRevenueDto
                {
                    Date = g.Key,
                    TotalAmount = g.Sum(o => o.TotalAmount ?? 0)
                })
                .ToListAsync();

            return Ok(new WeeklyMonthlyRevenueDto
            {
                LastWeekRevenue = lastWeekRevenue,
                ThisMonthRevenue = thisMonthRevenue
            });
        }
    }

    public class WeeklyMonthlyRevenueDto
    {
        public List<DailyRevenueDto> LastWeekRevenue { get; set; }
        public List<DailyRevenueDto> ThisMonthRevenue { get; set; }
    }

    public class DailyRevenueDto
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class MonthlyRevenueDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalAmount { get; set; }
    }
}