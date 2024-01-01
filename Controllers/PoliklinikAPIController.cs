using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevProje.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDevProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliklinikAPIController : ControllerBase
    {
        private readonly HastaneContext _context;

        public PoliklinikAPIController(HastaneContext context)
        {
            _context = context;
        }

        // GET: api/PoliklinikAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Poliklinik>>> GetPoliklinikler()
        {
            return await _context.Poliklinikler.ToListAsync();
        }

        // GET: api/PoliklinikAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Poliklinik>> GetPoliklinik(int id)
        {
            var poliklinik = await _context.Poliklinikler.FindAsync(id);

            if (poliklinik == null)
            {
                return NotFound();
            }

            return poliklinik;
        }

        // POST: api/PoliklinikAPI
        [HttpPost]
        public async Task<ActionResult<Poliklinik>> PostPoliklinik(Poliklinik poliklinik)
        {
            _context.Poliklinikler.Add(poliklinik);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoliklinik", new { id = poliklinik.Id }, poliklinik);
        }

        // PUT: api/PoliklinikAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoliklinik(int id, Poliklinik poliklinik)
        {
            if (id != poliklinik.Id)
            {
                return BadRequest();
            }

            _context.Entry(poliklinik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoliklinikExists(id))
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

        // DELETE: api/PoliklinikAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoliklinik(int id)
        {
            var poliklinik = await _context.Poliklinikler.FindAsync(id);
            if (poliklinik == null)
            {
                return NotFound();
            }

            _context.Poliklinikler.Remove(poliklinik);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PoliklinikExists(int id)
        {
            return _context.Poliklinikler.Any(e => e.Id == id);
        }
    }
}
