using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrawerPos.Data;
using DrawerPos.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DrawerPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DrawerPosDbContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(DrawerPosDbContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _context.Products
                                             .Include(p => p.Category)
                                             .ToListAsync();
                _logger.LogInformation("Fetched products successfully.");
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _context.Products
                                            .Include(p => p.Category)
                                            .FirstOrDefaultAsync(p => p.ProductId == id);

                if (product == null)
                {
                    _logger.LogWarning("Product not found: {Id}", id);
                    return NotFound();
                }

                _logger.LogInformation("Fetched product successfully: {Id}", id);
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the product with ID {Id}.", id);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductExists(id))
                {
                    _logger.LogWarning("Product not found for update: {Id}", id);
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, "Concurrency error while updating product: {Id}", id);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the product with ID {Id}.", id);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

            _logger.LogInformation("Updated product successfully: {Id}", id);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Created product successfully: {Id}", product.ProductId);
                return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the product.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product not found for deletion: {Id}", id);
                    return NotFound();
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Deleted product successfully: {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product with ID {Id}.", id);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string query)
        {
            try
            {
                var products = await _context.Products
                                             .Include(p => p.Category)
                                             .Where(p => p.ProductName.Contains(query) ||
                                                         p.Category.CategoryName.Contains(query) ||
                                                         p.Description.Contains(query))
                                             .ToListAsync();

                _logger.LogInformation($"Fetched {products.Count} products matching query '{query}'.");
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching filtered products.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        // New method for paginated data
        [HttpGet("paginated")]
        public async Task<ActionResult<PaginatedProducts>> GetProductsPaginated(int page = 1, int pageSize = 10)
        {
            try
            {
                var totalProducts = await _context.Products.CountAsync();
                var products = await _context.Products
                                    .Include(p => p.Category)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

                return new PaginatedProducts
                {
                    Products = products,
                    TotalProducts = totalProducts
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching paginated products.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
