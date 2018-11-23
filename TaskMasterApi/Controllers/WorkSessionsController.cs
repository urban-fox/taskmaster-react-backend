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
    public class WorkSessionsController : ControllerBase
    {
        private readonly TaskMasterApiContext _context;

        public WorkSessionsController(TaskMasterApiContext context)
        {
            _context = context;
        }

        // GET: api/WorkSessions
        [HttpGet]
        public IEnumerable<WorkSession> GetWorkSession()
        {
            return _context.WorkSession;
        }

        // GET: api/WorkSessions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkSession([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workSession = await _context.WorkSession.FindAsync(id);

            if (workSession == null)
            {
                return NotFound();
            }

            return Ok(workSession);
        }

        // PUT: api/WorkSessions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkSession([FromRoute] int id, [FromBody] WorkSession workSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workSession.WorkSessionId)
            {
                return BadRequest();
            }

            _context.Entry(workSession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkSessionExists(id))
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

        // POST: api/WorkSessions
        [HttpPost]
        public async Task<IActionResult> PostWorkSession([FromBody] WorkSession workSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WorkSession.Add(workSession);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkSession", new { id = workSession.WorkSessionId }, workSession);
        }

        // DELETE: api/WorkSessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkSession([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workSession = await _context.WorkSession.FindAsync(id);
            if (workSession == null)
            {
                return NotFound();
            }

            _context.WorkSession.Remove(workSession);
            await _context.SaveChangesAsync();

            return Ok(workSession);
        }

        private bool WorkSessionExists(int id)
        {
            return _context.WorkSession.Any(e => e.WorkSessionId == id);
        }
    }
}