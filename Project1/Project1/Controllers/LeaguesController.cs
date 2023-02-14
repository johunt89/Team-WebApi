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
        public async Task<ActionResult<LeagueDTO>> GetLeague(string id)
        {
            var league = await _context.Leagues
                .Include(t => t.Teams)
                .Select(l => new LeagueDTO
                {
                    Code = l.Code,
                    Name = l.Name,
                    Teams = l.Teams.Select(lTeam => new TeamDTO
                    {
                        ID = lTeam.ID,
                        Name = lTeam.Name,
                        Budget = lTeam.Budget
                    }).ToList()
                })
                .FirstOrDefaultAsync(i => i.Code == id);

            if (league == null)
            {
                return NotFound();
            }

            return league;
        }

        //PUT: api/Leagues/5    
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeague(string id, LeagueDTO leagueDTO)
        {
            if (id != leagueDTO.Code)
            {
                return BadRequest(new { message = "Error: Code Does nto match League" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var leagueToUpdate = await _context.Leagues.FindAsync(id);
            
            if(leagueToUpdate == null)
            {
                return NotFound(new { message = "Error: League record not found. " });
            }

            leagueToUpdate.Code = leagueDTO.Code;
            leagueToUpdate.Name = leagueDTO.Name;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeagueExists(id))
                {
                    return Conflict(new { message = "Concurrency Error: League has been Removed." });
                }
                else
                {
                    return Conflict(new { message = "Concurrency Error: League has been updated by another user." });
                }
            }
            catch (DbUpdateException dex)
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

        // POST: api/Leagues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<League>> PostLeague(LeagueDTO leagueDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            League league = new League
            {
                Code = leagueDTO.Code,
                Name = leagueDTO.Name
            };          
            try
            {
                _context.Leagues.Add(league);
                await _context.SaveChangesAsync();
                leagueDTO.Code = league.Code;
                return CreatedAtAction(nameof(GetLeague), new { code = league.Code }, leagueDTO);
            }
            catch (DbUpdateException dex)
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

        // DELETE: api/Leagues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeague(string id)
        {
            var league = await _context.Leagues.FindAsync(id);
            if (league == null)
            {
                return NotFound(new { message = "Delete Error: League has already been removed." });
            }
            try
            {
                _context.Leagues.Remove(league);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch(DbUpdateException)
            {
                return BadRequest(new { message = "Delete Error: Unable to delete league." });
            }
            
        }

        private bool LeagueExists(string id)
        {
            return _context.Leagues.Any(e => e.Code == id);
        }
    }
}
