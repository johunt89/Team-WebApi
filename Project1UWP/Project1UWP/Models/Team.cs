using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1UWP.Models
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Budget { get; set; }
        public string LeagueCode { get; set; }  
        public League League { get; set; }

        public ICollection<TeamPlayer> TeamPlayers { get; set; } = new HashSet<TeamPlayer>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (Name[0] == 'X' || Name[0] == 'F' || Name[0] == 'S')
            {
                yield return new ValidationResult("Team names are not allowed to start with the letters X, F, or S.", new[] { "Name" });
            }
        }
    }
}
