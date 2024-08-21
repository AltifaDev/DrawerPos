using Microsoft.AspNetCore.Mvc;
using DrawerPos.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrawerPos.Shared;
using DrawerPos.Data;
using Microsoft.EntityFrameworkCore;

namespace DrawerPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly DrawerPosDbContext _context;

        public UnitController(DrawerPosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            return await _context.Units.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(int id)
        {
            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return unit;
        }
        [HttpGet("by-ingredient/{ingredientId}")]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnitsByIngredient(int ingredientId)
        {
            var units = await _context.Units
                                       .Where(u => u.Ingredients.Any(i => i.IngredientId == ingredientId))
                                       .ToListAsync();

            return units;
        }
    }
}
