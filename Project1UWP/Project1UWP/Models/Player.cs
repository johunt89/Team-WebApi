using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1UWP.Models
{
    public class Player
    {
        public int ID { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string FormalName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int a = today.Year - DOB.Year
                    - ((today.Month < DOB.Month || (today.Month == DOB.Month && today.Day < DOB.Day) ? 1 : 0));
                return a; /*Note: You could add .PadLeft(3) but spaces disappear in a web page. */
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Jersey { get; set; }
        public DateTime DOB { get; set; }
        public double FeePaid { get; set; }
        public string EMail { get; set; }
        public Byte[] RowVersion { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; } = new List<TeamPlayer>();


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (DOB > DateTime.Today.AddDays(1))
        //    {
        //        yield return new ValidationResult("Date of Birth cannot be in the future.", new[] { "DOB" });
        //    }
        //    if (Age > 10 && FeePaid < 120.0)
        //    {
        //        yield return new ValidationResult("Players over 10 years old must pay a Fee of at least $120.", new[] { "FeePaid" });
        //    }

        //}
    }
}
