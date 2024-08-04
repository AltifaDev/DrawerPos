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
    public class DashboardController : ControllerBase
    {
        private readonly DrawerPosDbContext _context;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(DrawerPosDbContext context, ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Items)
                    .Select(o => new OrderDTO
                    {
                        OrderID = o.OrderId,
                        BillNo = o.BillNo,
                        OrderDate = o.OrderDate,
                        Total = o.Total,
                        Discount = o.Discount,
                        Items = o.Items.Select(i => new OrderItemDTO
                        {
                            OrderItemId = i.OrderItemId,
                            BillNo = i.BillNo,
                            //ProductId = i.ProductId,
                            ProductName = i.Product.ProductName,
                            Image = i.Product.Image,
                            NameDisplay = i.Product.NameDisplay,
                            Quantity = i.Quantity,
                            Price = i.Price,
                            Discount = i.Discount
                        }).ToList(),
                        CustomerId = o.CustomerId,
                        StoreId = o.StoreId,
                        PaymentMethod = o.PaymentMethod,
                        SubTotal = o.SubTotal,
                        Payments = o.Payments.Select(p => new PaymentDTO
                        {
                            PaymentId = p.PaymentId,
                            Amount = p.Amount ?? 0,
                            PaymentDate = p.PaymentDate ?? DateTime.Now,
                            PaymentMethod = p.PaymentMethod ?? "",
                            BillNo = p.BillNo,
                        }).ToList()
                    }).ToListAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching orders.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("date-orders")]
        public async Task<ActionResult<IEnumerable<GroupedOrderItem>>> GetDateOrders([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var query = _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .AsQueryable();

                if (startDate.HasValue)
                {
                    query = query.Where(o => o.OrderDate >= startDate);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(o => o.OrderDate <= endDate);
                }

                var filteredOrders = await query.ToListAsync();

                var groupedOrderItems = filteredOrders
                    .SelectMany(o => o.Items)
                    .GroupBy(oi => oi.Product)
                    .Select(CreateGroupedOrderItem)
                    .OrderBy(g => g.ProductName)
                    .ToList();

                return Ok(groupedOrderItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching date orders.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private GroupedOrderItem CreateGroupedOrderItem(IGrouping<Product?, OrderItem> productGroup)
        {
            var product = productGroup.Key!;
            return new GroupedOrderItem
            {
                ProductName = product.ProductName!,
                TotalQuantity = productGroup.Sum(oi => oi.Quantity ?? 0),
                TotalPrice = productGroup.Sum(oi => (oi.Price ?? 0) * (oi.Quantity ?? 0))
            };
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
                        TotalAmount = g.Sum(o => o.Total ?? 0)
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
                        TotalAmount = g.Sum(o => o.Total ?? 0)
                    })
                    .ToListAsync();

                var thisMonthRevenue = await _context.Orders
                    .Where(o => o.OrderDate >= startOfMonth)
                    .GroupBy(o => o.OrderDate.Date)
                    .Select(g => new DailyRevenueDto
                    {
                        Date = g.Key,
                        TotalAmount = g.Sum(o => o.Total ?? 0)
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
}
