using Project1UWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1UWP.Data
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetTeams();
        Task<Team> GetTeam(int ID);
        Task<List<Team>> GetTeamsByLeague(string leagueCode);
        Task<List<Team>> GetTeamPlayerCounts();

        Task AddTeam(Team teamToAdd);
        Task UpdateTeam(Team teamToUpdate);
        Task DeleteTeam(Team teamToDelete);

    }
}
