using Project_1.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project1.Models
{
    public class PlayerMetaData : Auditable
    {

        [Display(Name = "Player")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the first name blank.")]
        [StringLength(30, ErrorMessage = "First name cannot be more than 30 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the last name blank.")]
        [StringLength(50, ErrorMessage = "Last name cannot be more than 50 characters long.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You cannot leave the Jersey Number blank.")]
        [RegularExpression("^\\d{2}$", ErrorMessage = "Jersey Number must be 2 numeric digits.")]
        [StringLength(2, ErrorMessage = "Jersey Number must be 2 numeric digits.")]
        public string Jersey { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "You cannot leave the Date of Birth blank.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Display(Name = "Fee Paid")]
        [Required(ErrorMessage = "You cannot leave the fee amount blank.")]
        [DataType(DataType.Currency)]
        public double FeePaid { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [ScaffoldColumn(false)]
        [Timestamp]
        public Byte[] RowVersion { get; set; }

        [Display(Name = "Team")]
        public ICollection<TeamPlayer> TeamPlayers { get; set; } = new List<TeamPlayer>();

    }
}
