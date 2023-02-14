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
    public class PlayersController : ControllerBase
    {
        private readonly ProjectContext _context;

        public PlayersController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayers()
        {
            return await _context.Players
                .Include(p => p.TeamPlayers)
                .ThenInclude(t => t.Team)
                .Select(p => new PlayerDTO
                {
                    ID = p.ID,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Jersey = p.Jersey,
                    DOB = p.DOB,
                    FeePaid = p.FeePaid,
                    EMail = p.EMail,
                    TeamCount = p.TeamPlayers.Count(),
                    Teams = p.TeamPlayers.Select(t => new TeamDTO
                    {
                        ID = t.Team.ID,
                        Name = t.Team.Name
                    }).ToList()
                })
                .ToListAsync();
        }

        [HttpGet("ByTeam/{id}")]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayerTeamCounts(int id)
        {
            return await _context.Players
                .Include(p => p.TeamPlayers)
                .ThenInclude(p => p.Team)
                .Select(p => new PlayerDTO
                {
                    ID = p.ID,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Jersey = p.Jersey,
                    DOB = p.DOB,
                    FeePaid = p.FeePaid,
                    EMail = p.EMail,
                    TeamCount = p.TeamPlayers.Count(),
                    Teams = p.TeamPlayers.Select(t => new TeamDTO
                    {
                        ID = t.Team.ID,
                        Name = t.Team.Name
                    }).ToList()
                })
                .Where(l => l.ID == id)
                .ToListAsync();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, PlayerDTO playerDTO)
        {
            if (id != playerDTO.ID)
            {
                return BadRequest(new { message = "Error: ID Does nto match Player"});
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var playerToUpdate = await _context.Players.FindAsync(id);

            if(playerToUpdate == null)
            {
                return NotFound(new { message = "Error: Player record not found. " });
            }

            playerToUpdate.ID = playerDTO.ID;
            playerToUpdate.FirstName = playerDTO.FirstName;
            playerToUpdate.LastName = playerDTO.LastName;
            playerToUpdate.Jersey = playerDTO.Jersey;
            playerToUpdate.DOB = playerDTO.DOB;
            playerToUpdate.FeePaid = playerDTO.FeePaid;
            playerToUpdate.EMail = playerDTO.EMail;

            _context.Entry(playerToUpdate).Property("RowVersion").OriginalValue = playerDTO.RowVersion;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return Conflict(new {message = "Concurrency Error: Player has been Removed." });
                }
                else
                {
                    return Conflict(new { message = "Concurrency Error: Player has been updated by another user." });
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

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(PlayerDTO playerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Player player = new Player
            {
                ID = playerDTO.ID,
                FirstName = playerDTO.FirstName,
                LastName = playerDTO.LastName,
                Jersey = playerDTO.Jersey,
                DOB = playerDTO.DOB,
                FeePaid = playerDTO.FeePaid,
                EMail = playerDTO.EMail,
            };
            try
            {
                _context.Players.Add(player);
                await _context.SaveChangesAsync();
                playerDTO.ID = player.ID;
                return CreatedAtAction(nameof(GetPlayer), new { id = player.ID }, playerDTO);
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

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound(new { message = "Delete Error: Player has already been removed." });
            }
            try
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch(DbUpdateException)
            {
                return BadRequest(new { message = "Delete Error: Unable to delete player." });
            }
            
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.ID == id);
        }
    }
}
