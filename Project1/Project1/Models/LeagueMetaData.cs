using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project1.Models
{
    public class LeagueMetaData
    {
        [Required(ErrorMessage = "You cannot leave the league code blank.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Jersey Number must be 2 letters.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "League code must be 2 letters")]
        public string Code { get; set; }

        [Display(Name = "League Name")]
        [Required(ErrorMessage = "You cannot leave the league name blank.")]
        [StringLength(50, ErrorMessage = "League name cannot be more than 50 characters long.")]
        public string Name { get; set; }

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
