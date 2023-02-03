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
    public class TeamsController : ControllerBase
    {
        private readonly ProjectContext _context;

        public TeamsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams()
        {
            return await _context.Teams
                .Select(t => new TeamDTO
                {
                    ID = t.ID,
                    Name = t.Name,
                    Budget = t.Budget,
                    LeagueCode = t.LeagueCode,
                    League = new LeagueDTO
                    {
                        Name = t.League.Name
                    },
                })
                .ToListAsync();
        }

        [HttpGet("filterByLeague")]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeamsPlayersByLeague()
        {
            return await _context.Teams
                .Include(p => p.TeamPlayers)
                .ThenInclude(p => p.Player)
                .Select(t => new TeamDTO
                {
                    ID = t.ID,
                    Name = t.Name,
                    Budget = t.Budget,
                    LeagueCode = t.LeagueCode,
                    PlayerCount = t.TeamPlayers.Count(),
                    Players = t.TeamPlayers.Select( p => new PlayerDTO
                    {
                        FirstName = p.Player.FirstName,
                        LastName = p.Player.LastName,
                        DOB = p.Player.DOB,
                        FeePaid = p.Player.FeePaid
                    }).ToList()
                })
                .ToListAsync();
        }

        [HttpGet("TeamPlayerCount")]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeamPlayerCounts()
        {
            return await _context.Teams
                .Include(p => p.TeamPlayers)
                .ThenInclude(p => p.Player)
                .Select(t => new TeamDTO
                {
                    ID = t.ID,
                    Name = t.Name,
                    Budget = t.Budget,
                    LeagueCode = t.LeagueCode,
                    PlayerCount = t.TeamPlayers.Count(),
                    Players = t.TeamPlayers.Select(p => new PlayerDTO
                    {
                        FirstName = p.Player.FirstName,
                        LastName = p.Player.LastName,
                        DOB = p.Player.DOB,
                        FeePaid = p.Player.FeePaid
                    }).ToList()
                })
                .ToListAsync();
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.ID)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // POST: api/Teams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.ID }, team);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.ID == id);
        }
    }
}
