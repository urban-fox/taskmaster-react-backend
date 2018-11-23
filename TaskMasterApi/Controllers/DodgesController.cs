using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskMasterApi.Models;

namespace TaskMasterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DodgesController : ControllerBase
    {
        private readonly TaskMasterApiContext _context;

        public DodgesController(TaskMasterApiContext context)
        {
            _context = context;
        }

        // GET: api/Dodges
        [HttpGet]
        public IEnumerable<Dodge> GetDodge()
        {
            return _context.Dodge;
        }

        // GET: api/Dodges/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDodge([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dodge = await _context.Dodge.FindAsync(id);

            if (dodge == null)
            {
                return NotFound();
            }

            return Ok(dodge);
        }

        // PUT: api/Dodges/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDodge([FromRoute] int id, [FromBody] Dodge dodge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dodge.DodgeId)
            {
                return BadRequest();
            }

            _context.Entry(dodge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DodgeExists(id))
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

        // POST: api/Dodges
        [HttpPost]
        public async Task<IActionResult> PostDodge([FromBody] Dodge dodge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Dodge.Add(dodge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDodge", new { id = dodge.DodgeId }, dodge);
        }

        // DELETE: api/Dodges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDodge([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dodge = await _context.Dodge.FindAsync(id);
            if (dodge == null)
            {
                return NotFound();
            }

            _context.Dodge.Remove(dodge);
            await _context.SaveChangesAsync();

            return Ok(dodge);
        }

        private bool DodgeExists(int id)
        {
            return _context.Dodge.Any(e => e.DodgeId == id);
        }
    }
}