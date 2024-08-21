using Microsoft.AspNetCore.Mvc;
using DrawerPos.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DrawerPos.Data;

namespace DrawerPos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly DrawerPosDbContext _context;

        public IngredientsController(DrawerPosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
        {
            return await _context.Ingredients.Include(i => i.Unit).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id)
        {
            var ingredient = await _context.Ingredients.Include(i => i.Unit)
                                                     .FirstOrDefaultAsync(i => i.IngredientId == id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return ingredient;
        }

        [HttpPost("AddEdit")]
        public async Task<ActionResult<IngredientCreateDto>> PostIngredient(IngredientCreateDto ingredientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Log detailed validation errors
            }

            var ingredient = new Ingredient
            {
                IngredientName = ingredientDto.IngredientName,
                UnitId = ingredientDto.UnitId
            };

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            ingredientDto.IngredientId = ingredient.IngredientId;

            return CreatedAtAction(nameof(GetIngredient), new { id = ingredient.IngredientId }, ingredientDto);
        }

        [HttpPut("AddEdit/{id}")]
        public async Task<IActionResult> PutIngredient(int id, IngredientCreateDto ingredientDto)
        {
            if (id != ingredientDto.IngredientId)
            {
                return BadRequest();
            }

            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            ingredient.IngredientName = ingredientDto.IngredientName;
            ingredient.UnitId = ingredientDto.UnitId;

            _context.Entry(ingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
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
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(e => e.IngredientId == id);
        }
    }
}
