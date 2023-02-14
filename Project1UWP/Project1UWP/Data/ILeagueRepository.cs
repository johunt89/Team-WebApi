using Project1UWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1UWP.Data
{
    public interface ILeagueRepository
    {
        Task<List<League>> GetLeagues();
        Task<League> GetLeague(int leagueCode);
    }
}
