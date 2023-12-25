using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevProje.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDevProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnabilimDaliAPIController : ControllerBase
    {
        private readonly HastaneContext _context;

        public AnabilimDaliAPIController(HastaneContext context)
        {
            _context = context;
        }

        // GET: api/AnabilimDali
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnabilimDali>>> GetAnabilimDallari()
        {
            var anabilimDallari = await _context.AnabilimDallari.Include(a => a.Yonetici).ToListAsync();
            return Ok(anabilimDallari);
        }

        // GET: api/AnabilimDali/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnabilimDali>> GetAnabilimDali(int id)
        {
            var anabilimDali = await _context.AnabilimDallari.Include(a => a.Yonetici).FirstOrDefaultAsync(m => m.Id == id);

            if (anabilimDali == null)
            {
                return NotFound();
            }

            return Ok(anabilimDali);
        }

        // POST: api/AnabilimDali
        [HttpPost]
        public async Task<ActionResult<AnabilimDali>> PostAnabilimDali([FromBody] AnabilimDali anabilimDali)
        {
            if (ModelState.IsValid)
            {
                _context.AnabilimDallari.Add(anabilimDali);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAnabilimDali), new { id = anabilimDali.Id }, anabilimDali);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/AnabilimDali/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnabilimDali(int id, [FromBody] AnabilimDali anabilimDali)
        {
            if (id != anabilimDali.Id)
            {
                return BadRequest();
            }

            _context.Entry(anabilimDali).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnabilimDaliExists(id))
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

        // DELETE: api/AnabilimDali/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnabilimDali(int id)
        {
            var anabilimDali = await _context.AnabilimDallari.FindAsync(id);

            if (anabilimDali == null)
            {
                return NotFound();
            }

            _context.AnabilimDallari.Remove(anabilimDali);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnabilimDaliExists(int id)
        {
            return _context.AnabilimDallari.Any(e => e.Id == id);
        }
    }
}
