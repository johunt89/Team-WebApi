using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Diagnostics;
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

        [HttpGet("ByLeague/{id}")]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeamsPlayersByLeague(string id)
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
                    PlayerCount = t.TeamPlayers.Count,
                    Players = t.TeamPlayers.Select(p => new PlayerDTO
                    {
                        FirstName = p.Player.FirstName,
                        LastName = p.Player.LastName,
                        DOB = p.Player.DOB,
                        FeePaid = p.Player.FeePaid
                    }).ToList()
                })
                .Where(l => l.LeagueCode.ToLower() == id.ToLower())
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
        public async Task<IActionResult> PutTeam(int id, TeamDTO teamDTO)
        {
            if (id != teamDTO.ID)
            {
                return BadRequest(new { message = "Error: ID does not match Team" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teamToUpdate = await _context.Teams.FindAsync(id);

            if(teamToUpdate == null)
            {
                return NotFound(new { mesage = "Error: Team record not found. " });
            }

            teamToUpdate.ID = teamDTO.ID;
            teamToUpdate.Name = teamDTO.Name;
            teamToUpdate.Budget = teamDTO.Budget ?? throw new ArgumentNullException(nameof(teamDTO), "The value of 'teamDTO.Budget' should not be null");
            teamToUpdate.LeagueCode = teamDTO.LeagueCode ?? throw new ArgumentNullException(nameof(teamDTO), "The value of 'teamDTO.LeagueCode' should not be null");

            _context.Entry(teamToUpdate).Property("RowVersion").OriginalValue = teamDTO.RowVersion;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return Conflict(new { message = "Concurrency Error: Team has been Removed." });
                }
                else
                {
                    return Conflict(new { message = "Concurrency Error: Team has been updated by another user." });
                }
            }
            catch(DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE"))
                {
                    return BadRequest(new { message = "Unable to save: Duplicate data" });
                }
                else
                {
                    return BadRequest(new { message = "Unable to save changes to database" });
                }
            }
        }

        // POST: api/Teams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(TeamDTO teamDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Team team = new Team
            {
                ID = teamDTO.ID,
                Name = teamDTO.Name,
                Budget = teamDTO.Budget ?? throw new ArgumentNullException(nameof(teamDTO), "The value of 'teamDTO.Budget' should not be null"),
                LeagueCode = teamDTO.LeagueCode ?? throw new ArgumentNullException(nameof(teamDTO), "The value of 'teamDTO.LeagueCode' should not be null"),

            };

            try
            {
                _context.Teams.Add(team);
                await _context.SaveChangesAsync();

                teamDTO.ID = team.ID;

                return CreatedAtAction(nameof(GetTeam), new { id = team.ID }, teamDTO);
            }
            catch(DbUpdateException dex) 
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE"))
                {
                    return BadRequest(new { message = "Unable to save: Duplicate data" });
                }
                else
                {
                    return BadRequest(new { message = "Unable to save changes to database" });
                }
            }
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound(new { message = "Delete Error: Team has already been removed." });
            }
            try
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Delete Error: Unable to delete team." });
            }
            
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.ID == id);
        }
    }
}
