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
    public class RoleClaimsController : ControllerBase
    {
        private readonly CheckQlgiaoVuContext _context;

        public RoleClaimsController(CheckQlgiaoVuContext context)
        {
            _context = context;
        }

        // GET: api/RoleClaims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetRoleClaims()
        {
          if (_context.RoleClaims == null)
          {
              return NotFound();

          }
          
       
          
                        
            return await _context.RoleClaims.Select(roleClaim => new {
                id = roleClaim.Id,
            name = roleClaim.Name,
            describe = roleClaim.Describe,
            claim = roleClaim.Claim,

          }).ToListAsync();;
        }

        // GET: api/RoleClaims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleClaim>> GetRoleClaim(int id)
        {
          if (_context.RoleClaims == null)
          {
              return NotFound();
          }
            var roleClaim = await _context.RoleClaims.FindAsync(id);

            if (roleClaim == null)
            {
                return NotFound();
            }

            return roleClaim;
        }

        // PUT: api/RoleClaims/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleClaim(int id, RoleClaim roleClaim)
        {
            if (id != roleClaim.Id)
            {
                return BadRequest();
            }

            _context.Entry(roleClaim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleClaimExists(id))
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

        // POST: api/RoleClaims
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoleClaim>> PostRoleClaim(RoleClaim roleClaim)
        {
          if (_context.RoleClaims == null)
          {
              return Problem("Entity set 'CheckQlgiaoVuContext.RoleClaims'  is null.");
          }
            _context.RoleClaims.Add(roleClaim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoleClaim", new { id = roleClaim.Id }, roleClaim);
        }

        // DELETE: api/RoleClaims/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleClaim(int id)
        {
            if (_context.RoleClaims == null)
            {
                return NotFound();
            }
            var roleClaim = await _context.RoleClaims.FindAsync(id);
            if (roleClaim == null)
            {
                return NotFound();
            }

            _context.RoleClaims.Remove(roleClaim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleClaimExists(int id)
        {
            return (_context.RoleClaims?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
