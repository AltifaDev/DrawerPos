// File: Controllers/ReceiptHeadersController.cs
using DrawerPos.Shared;
using DrawerPos.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ReceiptHeadersController : ControllerBase
{
    private readonly DrawerPosDbContext _context;

    public ReceiptHeadersController(DrawerPosDbContext context)
    {
        _context = context;
    }

    // GET: api/ReceiptHeaders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReceiptHeader>>> GetReceiptHeaders()
    {
        return await _context.ReceiptHeaders.ToListAsync();
    }

    // GET: api/ReceiptHeaders/latest
    [HttpGet("latest")]
    public async Task<ActionResult<ReceiptHeader>> GetLatestReceiptHeaderAsync()
    {
        var latestReceiptHeader = await _context.ReceiptHeaders
                                       .OrderByDescending(r => r.Id)
                                       .FirstOrDefaultAsync();

        if (latestReceiptHeader == null)
        {
            return NotFound(); // Or appropriate action for when no record is found
        }

        return Ok(latestReceiptHeader);
    }

    // GET: api/ReceiptHeaders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ReceiptHeader>> GetReceiptHeader(int id)
    {
        var receiptHeader = await _context.ReceiptHeaders.FindAsync(id);
        if (receiptHeader == null)
        {
            return NotFound();
        }
        return receiptHeader;
    }

    // POST: api/ReceiptHeaders
    [HttpPost]
    public async Task<ActionResult<ReceiptHeader>> PostReceiptHeader(ReceiptHeader receiptHeader)
    {
        _context.ReceiptHeaders.Add(receiptHeader);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetReceiptHeader), new { id = receiptHeader.Id }, receiptHeader);
    }

    // PUT: api/ReceiptHeaders/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReceiptHeader(int id, ReceiptHeader receiptHeader)
    {
        if (id != receiptHeader.Id)
        {
            return BadRequest();
        }
        _context.Entry(receiptHeader).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ReceiptHeaders.Any(e => e.Id == id))
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

    // DELETE: api/ReceiptHeaders/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReceiptHeader(int id)
    {
        var receiptHeader = await _context.ReceiptHeaders.FindAsync(id);
        if (receiptHeader == null)
        {
            return NotFound();
        }
        _context.ReceiptHeaders.Remove(receiptHeader);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
