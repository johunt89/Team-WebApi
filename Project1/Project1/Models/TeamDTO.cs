using Microsoft.AspNetCore.Mvc;
using Project_1.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project1.Models
{
    [ModelMetadataType(typeof(TeamMetaData))]
    public class TeamDTO
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public double Budget { get; set; }
        public string LeagueCode { get; set; }
        public LeagueDTO League { get; set; }


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
