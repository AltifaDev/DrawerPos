using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DrawerPos.Shared;
using DrawerPos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DrawerPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DrawerPosDbContext _context;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(DrawerPosDbContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDto)
        {
            var order = new Order
            {
                BillNo = await GenerateBillNumber(),
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
                StoreId = orderDto.StoreId,
                TotalAmount = orderDto.Total,
                TotalDiscount = orderDto.Discount,
                Discount = orderDto.Discount,
                PaymentMethod = orderDto.PaymentMethod,
                SubTotal = orderDto.SubTotal,
                Total = orderDto.Total
            };

            foreach (var itemDto in orderDto.Items)
            {
                order.Items.Add(new OrderItem
                {
                    OrderItemId = itemDto.OrderItemId,
                    BillNo = itemDto.BillNo,
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    Price = itemDto.Price,
                    Discount = itemDto.Discount
                });
            }

            foreach (var paymentDto in orderDto.Payments)
            {
                order.Payments.Add(new Payment
                {
                    PaymentId = paymentDto.PaymentId,
                    BillNo = paymentDto.BillNo,
                    PaymentDate = paymentDto.PaymentDate,
                    Amount = paymentDto.Amount,
                    PaymentMethod = paymentDto.PaymentMethod
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrderById), new { id = order.BillNo }, order);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(string id)
        {
            var order = await _context.Orders.Include(o => o.Items)
                                              .FirstOrDefaultAsync(o => o.BillNo == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }
         
        private async Task<string> GenerateBillNumber()
        {
            var lastBillNumber = await GetLastBillNumber();
            var newBillNumber = IncrementBillNumber(lastBillNumber);
            await UpdateLastBillNumber(newBillNumber);
            return newBillNumber;
        }

        private async Task<string> GetLastBillNumber()
        {
            try
            {
                var lastBillNumber = await _context.BillNumbers
                    .OrderByDescending(b => b.BillNo)
                    .Select(b => b.BillNo)
                    .FirstOrDefaultAsync();

                return lastBillNumber ?? "#00000000";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching last BillNo.");
                throw new Exception("Internal server error while fetching last BillNo.");
            }
        }

        private string IncrementBillNumber(string billNo)
        {
            if (!string.IsNullOrEmpty(billNo))
            {
                if (int.TryParse(billNo.Substring(1), out int number))
                {
                    number++;
                    return "#" + number.ToString("D8");
                }
            }

            return "#00000001";
        }

        private async Task UpdateLastBillNumber(string newBillNumber)
        {
            var billNumber = new BillNumber { BillNo = newBillNumber };
            _context.BillNumbers.Add(billNumber);
            await _context.SaveChangesAsync();
        }

        [HttpGet("last-billno")]
        public async Task<IActionResult> GetLastBillNo()
        {
            try
            {
                var lastBillNo = await _context.BillNumbers
                    .OrderByDescending(b => b.BillNo)
                    .Select(b => b.BillNo)
                    .FirstOrDefaultAsync();

                if (lastBillNo == null)
                {
                    return NotFound("No bill numbers found.");
                }

                return Ok(lastBillNo);
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
                    .Include(o => o.Items)
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

                var orders = await query
                    .Include(o => o.Items)
                    .ThenInclude(oi => oi.Product)
                    .ToListAsync();

                var groupedOrderItems = orders
                    .SelectMany(o => o.Items)
                    .GroupBy(oi => oi.Product)
                    .Select(g => new GroupedOrderItem
                    {
                        Products = g.Key,
                        TotalQuantity = g.Sum(oi => oi.Quantity ?? 0),
                        TotalPrice = g.Sum(oi => (oi.Price) * oi.Quantity ?? 0)
                    })
                    .OrderBy(g => g.Products.NameDisplay)
                    .ToList();

                return Ok(groupedOrderItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching date orders.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("monthly-revenue")]
        public async Task<ActionResult<IEnumerable<MonthlyRevenueDto>>> GetMonthlyRevenue()
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching monthly revenue.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("weekly-monthly-revenue")]
        public async Task<ActionResult<WeeklyMonthlyRevenueDto>> GetWeeklyMonthlyRevenue()
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching weekly and monthly revenue.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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

    public class GroupedOrderItem
    {
        public Product Products { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

}