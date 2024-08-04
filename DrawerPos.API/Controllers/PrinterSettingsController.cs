// Controllers/PrinterSettingsController.cs
using DrawerPos.Data;
using DrawerPos.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrawerPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrinterSettingsController : ControllerBase
    {
        private readonly DrawerPosDbContext _context;

        public PrinterSettingsController(DrawerPosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrinterSetting>>> GetPrinterSettings()
        {
            return await _context.PrinterSettings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrinterSetting>> GetPrinterSetting(int id)
        {
            var printerSetting = await _context.PrinterSettings.FindAsync(id);

            if (printerSetting == null)
            {
                return NotFound();
            }

            return printerSetting;
        }

        [HttpPost]
        public async Task<ActionResult<PrinterSetting>> PostPrinterSetting(PrinterSetting printerSetting)
        {
            _context.PrinterSettings.Add(printerSetting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrinterSetting", new { id = printerSetting.Id }, printerSetting);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrinterSetting(int id, PrinterSetting printerSetting)
        {
            if (id != printerSetting.Id)
            {
                return BadRequest();
            }

            _context.Entry(printerSetting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrinterSettingExists(id))
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
        public async Task<IActionResult> DeletePrinterSetting(int id)
        {
            var printerSetting = await _context.PrinterSettings.FindAsync(id);
            if (printerSetting == null)
            {
                return NotFound();
            }

            _context.PrinterSettings.Remove(printerSetting);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrinterSettingExists(int id)
        {
            return _context.PrinterSettings.Any(e => e.Id == id);
        }
    }
}
