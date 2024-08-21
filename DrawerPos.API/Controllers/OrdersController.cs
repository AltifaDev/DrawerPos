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
using System.Data.SqlTypes;

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
                order.OrderItems.Add(new OrderItem
                {
                    OrderItemId = itemDto.OrderItemId,
                    BillNo = itemDto.BillNo,
                    ProductId = itemDto.ProductId ?? 0,
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
            var order = await _context.Orders.Include(o => o.OrderItems)
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

                // Handle null lastBillNumber and invalid format
                if (string.IsNullOrEmpty(lastBillNumber) || !lastBillNumber.StartsWith("#"))
                {
                    return "#00000000"; // Start with a default value
                }

                return lastBillNumber;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching last BillNo.");
                throw new Exception("Internal server error while fetching last BillNo."); // Re-throw for consistent error handling
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
            try
            {
                var existingBillNumber = await _context.BillNumbers.FirstOrDefaultAsync();

                // *** Check if existingBillNumber is null ***
                if (existingBillNumber == null)
                {
                    _context.BillNumbers.Add(new BillNumber { BillNo = newBillNumber });
                }
                else
                {
                    // ... (your existing update logic) ...

                    existingBillNumber.BillNo = newBillNumber;
                    _context.BillNumbers.Update(existingBillNumber);
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency conflict (e.g., retry, log, or inform the user)
                    _logger.LogWarning("Concurrency conflict detected while updating last BillNo. Retrying...");

                    // Option 1: Retry the update (consider limiting the number of retries)
                    await UpdateLastBillNumber(newBillNumber); // Recursively retry

                    // Option 2: Log and return an error
                    // _logger.LogError("Concurrency conflict resolved by not updating.");
                    // throw new Exception("Internal server error: Concurrency conflict while updating bill number.");
                }

                // Explicitly reload the entity to ensure the updated value is fetched from the database
                await _context.Entry(existingBillNumber).ReloadAsync();

                // Validation after saving and reloading
                if (existingBillNumber == null || string.IsNullOrEmpty(existingBillNumber.BillNo))
                {
                    _logger.LogError("BillNo is still null or empty after updating and reloading.");
                    throw new Exception("Internal server error: Failed to update bill number.");
                }
            }
            catch (DbUpdateException dbEx)
            {
                // Catch specific database update exceptions
                _logger.LogError(dbEx, "Database error occurred while updating last BillNo.");
                throw new Exception("Internal server error: Database error while updating bill number.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while updating last BillNo.");
                throw new Exception("Internal server error: Unexpected error while updating bill number.");
            }
        }


        [HttpGet("last-billno")]
        public async Task<IActionResult> GetLastBillNo()
        {
            try
            {
          
                var lastBillNo = await _context.BillNumbers
                   
                   .Select(b => b.BillNo)
                   .ToListAsync();

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
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(i => i.Product) // Include Product navigation property
                    .Select(o => new OrderDTO
                    {
                        
                        BillNo = o.BillNo,
                        OrderDate = o.OrderDate,
                        Total = o.Total,
                        Discount = o.Discount,
                        Items = o.OrderItems.Select(i => new OrderItemDTO
                        {
                            OrderItemId = i.OrderItemId,
                            BillNo = i.BillNo,
                            ProductId = i.ProductId,
                            ProductName = i.Product.ProductName,
                            Quantity = i.Quantity,
                            Price = i.Price,
                            Discount = i.Discount
                        }).ToList(),
                        Payments = o.Payments.Select(p => new PaymentDTO
                        {
                            // Map PaymentDTO fields here
                        }).ToList()
                    })
                    .ToListAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching orders.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
