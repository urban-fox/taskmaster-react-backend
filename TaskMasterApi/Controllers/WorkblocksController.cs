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
    public class WorkBlocksController : ControllerBase
    {
        private readonly TaskMasterApiContext _context;

        public WorkBlocksController(TaskMasterApiContext context)
        {
            _context = context;
        }

        // GET: api/WorkBlocks
        [HttpGet]
        public IEnumerable<WorkBlock> GetWorkblock()
        {
            return _context.Workblock;
        }

        // GET: api/WorkBlocks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkBlock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workBlock = await _context.Workblock.FindAsync(id);

            if (workBlock == null)
            {
                return NotFound();
            }

            return Ok(workBlock);
        }

        // PUT: api/WorkBlocks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkBlock([FromRoute] int id, [FromBody] WorkBlock workBlock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workBlock.WorkBlockId)
            {
                return BadRequest();
            }

            _context.Entry(workBlock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkBlockExists(id))
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

        // POST: api/WorkBlocks
        [HttpPost]
        public async Task<IActionResult> PostWorkBlock([FromBody] WorkBlock workBlock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Workblock.Add(workBlock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkBlock", new { id = workBlock.WorkBlockId }, workBlock);
        }

        // DELETE: api/WorkBlocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkBlock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workBlock = await _context.Workblock.FindAsync(id);
            if (workBlock == null)
            {
                return NotFound();
            }

            _context.Workblock.Remove(workBlock);
            await _context.SaveChangesAsync();

            return Ok(workBlock);
        }

        private bool WorkBlockExists(int id)
        {
            return _context.Workblock.Any(e => e.WorkBlockId == id);
        }

        public object GetNext()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkSession> GetNext(DateTime today)
        {
            throw new NotImplementedException();
        }
    }
}