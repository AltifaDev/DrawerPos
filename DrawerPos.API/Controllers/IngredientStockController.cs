using Microsoft.AspNetCore.Mvc;
using DrawerPos.Shared;
using DrawerPos.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DrawerPos.Shared.Models;

namespace DrawerPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientStockController : ControllerBase
    {
        private readonly DrawerPosDbContext _context;

        public IngredientStockController(DrawerPosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientStock>>> GetIngredientStocks()
        {
            var ingredientStocks = await _context.IngredientStocks
                .Include(i => i.Ingredient)
                .ThenInclude(i => i.Unit)
                .ToListAsync();

            // Handle nulls in Ingredient and Unit gracefully
            foreach (var stock in ingredientStocks)
            {
                stock.Ingredient.IngredientName ??= "Unknown Ingredient";

                // Check for null Unit and handle accordingly
                if (stock.Ingredient.Unit == null)
                {
                    // Option 1: Assign a default Unit if appropriate for your application
                    stock.Ingredient.Unit = new Unit { UnitName = "Unknown Unit" };

                    // Option 2: Filter out IngredientStocks with null Units
                    // ingredientStocks.Remove(stock); 
                    // continue; // Skip to the next iteration

                    // Option 3: Throw an exception or log an error if null Units are unexpected
                    // throw new Exception("Ingredient has a null Unit!"); 
                }
            }

            return ingredientStocks;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientStock>> GetIngredientStock(int id)
        {
            var ingredientStock = await _context.IngredientStocks
                .Include(i => i.Ingredient)
                .ThenInclude(i => i.Unit)
                .FirstOrDefaultAsync(i => i.IngredientStockId == id);

            if (ingredientStock == null)
            {
                return NotFound();
            }

            // (Optional) Check if there's at least one ingredient in the unit's collection
            // You might not need this check depending on your application logic
            if (ingredientStock.Ingredient.Unit.Ingredients.Count > 0)
            {
                var firstIngredientInUnit = ingredientStock.Ingredient.Unit.Ingredients.FirstOrDefault();
                // ... (use firstIngredientInUnit as needed)
            }

            return ingredientStock;
        }

        [HttpPost]
        public async Task<ActionResult<IngredientStockDto>> PostIngredientStock(IngredientStockDto ingredientStockDto)
        {
            // Find the Ingredient associated with the IngredientId in the DTO
            var ingredient = await _context.Ingredients.FindAsync(ingredientStockDto.IngredientId);

            if (ingredient == null)
            {
                return BadRequest("Invalid IngredientId."); // Or handle this case appropriately
            }

            // Create a new IngredientStock object and map properties from the DTO
            var ingredientStock = new IngredientStock
            {
                IngredientId = ingredientStockDto.IngredientId,
                QuantityInStock = ingredientStockDto.QuantityInStock,
                ExpirationDate = ingredientStockDto.ExpirationDate,
                ReorderPoint = ingredientStockDto.ReorderPoint,
                Ingredient = ingredient // Associate the Ingredient object
            };

            _context.IngredientStocks.Add(ingredientStock);
            await _context.SaveChangesAsync();

            // Return the DTO with the generated IngredientStockId
            ingredientStockDto.IngredientStockId = ingredientStock.IngredientStockId;
            return CreatedAtAction(nameof(GetIngredientStock), new { id = ingredientStockDto.IngredientStockId }, ingredientStockDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredientStock(int id, IngredientStockDto ingredientStockDto)
        {
            if (id != ingredientStockDto.IngredientStockId)
            {
                return BadRequest();
            }

            // Find the existing IngredientStock
            var ingredientStock = await _context.IngredientStocks.FindAsync(id);

            if (ingredientStock == null)
            {
                return NotFound();
            }

            // Update properties from the DTO
            ingredientStock.IngredientId = ingredientStockDto.IngredientId;
            ingredientStock.QuantityInStock = ingredientStockDto.QuantityInStock;
            ingredientStock.ExpirationDate = ingredientStockDto.ExpirationDate;
            ingredientStock.ReorderPoint = ingredientStockDto.ReorderPoint;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientStockExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientStock(int id)
        {
            var ingredientStock = await _context.IngredientStocks.FindAsync(id);
            if (ingredientStock == null)
            {
                return NotFound();
            }

            _context.IngredientStocks.Remove(ingredientStock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientStockExists(int id)
        {
            return _context.IngredientStocks.Any(e => e.IngredientStockId == id);
        }
    }
}
