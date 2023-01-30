namespace Project1.Models
{
    public class League
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
