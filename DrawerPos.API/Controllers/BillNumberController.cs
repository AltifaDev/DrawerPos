using Microsoft.AspNetCore.Mvc;
using DrawerPos.Data;
using DrawerPos.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DrawerPos.Shared;

namespace DrawerPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillNumberController : ControllerBase
    {
        private readonly AltifaDbContext _context;

        public BillNumberController(AltifaDbContext context)
        {
            _context = context;
        }

        // GET: api/billnumber
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillNumber>>> GetBillNumbers()
        {
            try
            {
                return await _context.BillNumbers.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
