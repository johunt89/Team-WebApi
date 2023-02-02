using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    [MetadataType(typeof(LeagueMetaData))]
    public class LeagueDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int TeamCount { get; set; }


        public ICollection<TeamDTO> Teams { get; set; }
    }
}
