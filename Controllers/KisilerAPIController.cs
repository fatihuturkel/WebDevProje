using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevProje.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDevProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KisilerAPIController : ControllerBase
    {

        private readonly HastaneContext _context;

        public KisilerAPIController(HastaneContext context)
        {
            _context = context;
        }

        // GET: api/KisilerAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kisi>>> GetKisiler()
        {
            return await _context.Kisiler.ToListAsync();
        }

        // GET: api/KisilerAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kisi>> GetKisi(int id)
        {
            var kisi = await _context.Kisiler.FindAsync(id);

            if (kisi == null)
            {
                return NotFound();
            }

            return kisi;
        }

        // POST: api/KisilerAPI
        [HttpPost]
        public async Task<ActionResult<Kisi>> PostKisi(Kisi kisi)
        {
            _context.Kisiler.Add(kisi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKisi), new { id = kisi.Id }, kisi);
        }

        // PUT: api/KisilerAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKisi(int id, Kisi kisi)
        {
            if (id != kisi.Id)
            {
                return BadRequest();
            }

            _context.Entry(kisi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KisiExists(id))
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

        // DELETE: api/KisilerAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKisi(int id)
        {
            var kisi = await _context.Kisiler.FindAsync(id);
            if (kisi == null)
            {
                return NotFound();
            }

            _context.Kisiler.Remove(kisi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KisiExists(int id)
        {
            return _context.Kisiler.Any(e => e.Id == id);
        }
    }
}

