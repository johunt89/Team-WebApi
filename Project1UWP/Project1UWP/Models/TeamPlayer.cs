using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1UWP.Models
{
    public class TeamPlayer
    {
        public int TeamID { get; set; }
        public Team Team { get; set; }

        public int PlayerID { get; set; }
        public Player Player { get; set; }
    }
}
