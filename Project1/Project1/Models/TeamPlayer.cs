namespace Project1.Models
{
    public class TeamPlayer
    { 
        public int TeamID { get; set; }
        public Team Team { get; set; }

        public int PlayerID { get; set; }
        public Player Player { get; set; }
        //just like play class
        //change navigation properties in other classes once complete
        //player & team --> teamplayer collections

    }
}
