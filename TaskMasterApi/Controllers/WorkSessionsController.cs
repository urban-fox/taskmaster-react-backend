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
        [HttpGet("/next")]
        public IEnumerable<WorkSession> GetNext(DateTime today)
        {
            // return _context.WorkSession;
            // should return by priorty, date, then id

            IEnumerable<WorkSession> results = from ws in _context.WorkSession
                                               where ws.ScheduleAfter <= today
                                               orderby ws.Priority ascending,
                                                    ws.ScheduleAfter ascending,
                                                    ws.WorkSessionId ascending
                                               select ws;
            return results;
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