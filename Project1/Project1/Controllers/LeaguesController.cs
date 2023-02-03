using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly ProjectContext _context;

        public LeaguesController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Leagues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeagueDTO>>> GetLeagues()
        {
            return await _context.Leagues
                .Select(l => new LeagueDTO
                {
                    Code = l.Code,
                    Name = l.Name
                })
                .ToListAsync();
        }

        [HttpGet("LeagueTeams")]
        public async Task<ActionResult<IEnumerable<LeagueDTO>>> GetLeaguesTeams()
        {
            return await _context.Leagues
                .Include(t => t.Teams)
                .Select(l => new LeagueDTO
                {
                    Code = l.Code,
                    Name = l.Name,
                    TeamCount = l.Teams.Count(),
                    Teams = l.Teams.Select(l => new TeamDTO
                    {
                        Name = l.Name,
                        Budget = l.Budget
                    }).ToList()
                })
                .ToListAsync();
        }

        // GET: api/Leagues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<League>> GetLeague(string id)
        {
            var league = await _context.Leagues
                .Include(t => t.Teams)
                .FirstOrDefaultAsync(i => i.Code == id);

            if (league == null)
            {
                return NotFound();
            }

            return league;
        }

            
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeague(string id, League league)
        {
            if (id != league.Code)
            {
                return BadRequest();
            }

            _context.Entry(league).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeagueExists(id))
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

        // POST: api/Leagues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<League>> PostLeague(League league)
        {
            _context.Leagues.Add(league);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LeagueExists(league.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLeague", new { id = league.Code }, league);
        }

        // DELETE: api/Leagues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeague(string id)
        {
            var league = await _context.Leagues.FindAsync(id);
            if (league == null)
            {
                return NotFound();
            }

            _context.Leagues.Remove(league);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeagueExists(string id)
        {
            return _context.Leagues.Any(e => e.Code == id);
        }
    }
}
