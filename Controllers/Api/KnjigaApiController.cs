using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiblioMeta2.Data;
using BiblioMeta2.Models;

namespace BiblioMeta2.Controllers_Api
{
    [Route("api/knjiga")]
    [ApiController]
    public class KnjigaApiController : ControllerBase
    {
        private readonly Context _context;

        public KnjigaApiController(Context context)
        {
            _context = context;
        }

        // GET: api/KnjigaApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Knjiga>>> GetKnjiga()
        {
            return await _context.Knjiga.ToListAsync();
        }

        // GET: api/KnjigaApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Knjiga>> GetKnjiga(int id)
        {
            var knjiga = await _context.Knjiga.FindAsync(id);

            if (knjiga == null)
            {
                return NotFound();
            }

            return knjiga;
        }

        // PUT: api/KnjigaApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKnjiga(int id, Knjiga knjiga)
        {
            if (id != knjiga.KnjigaID)
            {
                return BadRequest();
            }

            _context.Entry(knjiga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KnjigaExists(id))
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

        // POST: api/KnjigaApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Knjiga>> PostKnjiga(Knjiga knjiga)
        {
            _context.Knjiga.Add(knjiga);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKnjiga", new { id = knjiga.KnjigaID }, knjiga);
        }

        // DELETE: api/KnjigaApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKnjiga(int id)
        {
            var knjiga = await _context.Knjiga.FindAsync(id);
            if (knjiga == null)
            {
                return NotFound();
            }

            _context.Knjiga.Remove(knjiga);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KnjigaExists(int id)
        {
            return _context.Knjiga.Any(e => e.KnjigaID == id);
        }
    }
}
