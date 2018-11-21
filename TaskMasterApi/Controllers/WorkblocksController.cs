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
    public class WorkblocksController : ControllerBase
    {
        private readonly TaskMasterApiContext _context;

        public WorkblocksController(TaskMasterApiContext context)
        {
            _context = context;
        }

        // GET: api/Workblocks
        [HttpGet]
        public IEnumerable<Workblock> GetWorkblock()
        {
            return _context.Workblock;
        }

        // GET: api/Workblocks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkblock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workblock = await _context.Workblock.FindAsync(id);

            if (workblock == null)
            {
                return NotFound();
            }

            return Ok(workblock);
        }

        // PUT: api/Workblocks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkblock([FromRoute] int id, [FromBody] Workblock workblock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workblock.Id)
            {
                return BadRequest();
            }

            _context.Entry(workblock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkblockExists(id))
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

        // POST: api/Workblocks
        [HttpPost]
        public async Task<IActionResult> PostWorkblock([FromBody] Workblock workblock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Workblock.Add(workblock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkblock", new { id = workblock.Id }, workblock);
        }

        // DELETE: api/Workblocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkblock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workblock = await _context.Workblock.FindAsync(id);
            if (workblock == null)
            {
                return NotFound();
            }

            _context.Workblock.Remove(workblock);
            await _context.SaveChangesAsync();

            return Ok(workblock);
        }

        private bool WorkblockExists(int id)
        {
            return _context.Workblock.Any(e => e.Id == id);
        }
    }
}