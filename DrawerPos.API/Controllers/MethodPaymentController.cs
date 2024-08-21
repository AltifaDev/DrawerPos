using DrawerPos.Data;
using DrawerPos.Shared;
using DrawerPos.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrawerPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MethodPaymentController : ControllerBase
    {
        private readonly DrawerPosDbContext _context;

        public MethodPaymentController(DrawerPosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MethodPayment>>> GetMethodPayments()
        { 
            var payments = await _context.MethodPayments.ToListAsync();
                return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MethodPayment>> GetMethodPayment(int id)
        {
            var methodPayment = await _context.MethodPayments.FindAsync(id);
            if (methodPayment == null)
            {
                return NotFound();
            }
             return methodPayment;
        }

        [HttpGet("PaymentsStatus")]
        public async Task<ActionResult<string>> GetMethodPaymentsStatus()
        {
            var methodPayment = await _context.MethodPayments
                .Where(p => p.MethodStatus == "Yes")
                .ToListAsync();

            if (methodPayment == null)
            {
                return NotFound();
            }
              return Ok(methodPayment);
        }
        [HttpPost]
        public async Task<ActionResult<MethodPayment>> PostMethodPayment(MethodPayment methodPayment)
        {
            _context.MethodPayments.Add(methodPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMethodPayment", new { id = methodPayment.QrId }, methodPayment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMethodPayment(int id, MethodPayment methodPayment)
        {
            if (id != methodPayment.QrId)
            {
                return BadRequest();
            }

            _context.Entry(methodPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MethodPaymentExists(id))
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
        public async Task<IActionResult> DeleteMethodPayment(int id)
        {
            var methodPayment = await _context.MethodPayments.FindAsync(id);
            if (methodPayment == null)
            {
                return NotFound();
            }

            _context.MethodPayments.Remove(methodPayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MethodPaymentExists(int id)
        {
            return _context.MethodPayments.Any(e => e.QrId == id);
        }
    }

}
