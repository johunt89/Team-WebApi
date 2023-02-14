using Project1UWP.Models;
using Project1UWP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using System.Net.Http.Headers;

namespace Project1UWP.Data
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly HttpClient client = new HttpClient();

        public LeagueRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<League>> GetLeagues()
        {
            HttpResponseMessage response = await client.GetAsync("api/Leagues");
            if (response.IsSuccessStatusCode)
            {
                List<League> leagues = await response.Content.ReadAsAsync<List<League>>();
                return leagues;
            }
            else
            {
                throw new Exception("Could not access the list of leagues.");
            }
        }

        public async Task<League> GetLeague(int leagueCode)
        {
            HttpResponseMessage response = await client.GetAsync($"api/Leagues/{leagueCode}");
            if (response.IsSuccessStatusCode)
            {
                League league = await response.Content.ReadAsAsync<League>();
                return league;
            }
            else
            {
                throw new Exception("Could not access league record");
            }
        }
    }
}
