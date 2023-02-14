using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1UWP.Models
{
    public class League
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? TeamCount { get; set; }
        public Byte[] RowVersion { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}
