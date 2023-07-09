using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnTruongTim.Models;

namespace DuAnTruongTim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequetsController : ControllerBase
    {
        private readonly CheckQlgiaoVuContext _context;

        public RequetsController(CheckQlgiaoVuContext context)
        {
            _context = context;
        }

        // GET: api/Requets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requet>>> GetRequets()
        {
          if (_context.Requets == null)
          {
              return NotFound();
          }
            return await _context.Requets.ToListAsync();
        }

        // GET: api/Requets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Requet>> GetRequet(int id)
        {
          if (_context.Requets == null)
          {
              return NotFound();
          }
            var requet = await _context.Requets.FindAsync(id);

            if (requet == null)
            {
                return NotFound();
            }

            return requet;
        }

        // PUT: api/Requets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequet(int id, Requet requet)
        {
            if (id != requet.Id)
            {
                return BadRequest();
            }

            _context.Entry(requet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequetExists(id))
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

        // POST: api/Requets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Requet>> PostRequet(Requet requet)
        {
          if (_context.Requets == null)
          {
              return Problem("Entity set 'CheckQlgiaoVuContext.Requets'  is null.");
          }
            _context.Requets.Add(requet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequet", new { id = requet.Id }, requet);
        }

        // DELETE: api/Requets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequet(int id)
        {
            if (_context.Requets == null)
            {
                return NotFound();
            }
            var requet = await _context.Requets.FindAsync(id);
            if (requet == null)
            {
                return NotFound();
            }

            _context.Requets.Remove(requet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequetExists(int id)
        {
            return (_context.Requets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
