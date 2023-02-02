using Project1.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;

namespace Project1.Data
{
    public class ProjectInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            ProjectContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetService<ProjectContext>();

            try
            {

                Random random = new Random();
                //League Data
                if (!context.Leagues.Any())
                {
                    context.Leagues.AddRange(
                        new League
                        {
                            Name = "Premier League",
                            Code = "PL"
                        },
                        new League 
                        { 
                            Name = "Major League",
                            Code = "ML"
                        },
                        new League
                        {
                            Name = "Pro League",
                            Code = "PP"
                        },
                        new League
                        {
                            Name = "Little League",
                            Code = "LL"
                        }
                        );
                    context.SaveChanges();
                }
                //Team Data
                if (!context.Teams.Any())
                {
                    context.Teams.AddRange(
                    new Team
                    {
                        Name = "Toronto Laughs", //assign leagues
                        LeagueCode = "PP"

                    },
                    new Team
                    {
                        Name = "Hamilton Steel",
                        LeagueCode = "LL"
                    },
                    new Team
                    {
                        Name = "Sudbury Nickles",
                        LeagueCode = "ML"
                    },
                    new Team
                    {
                        Name = "Winnipeg Moose",
                        LeagueCode = "PP"
                    },
                    new Team
                    {
                        Name = "Saskatoon Flatlands",
                        LeagueCode = "PL"
                    }
                    ,
                    new Team
                    {
                        Name = "Vancouver Whales",
                        LeagueCode = "LL"
                    },
                    new Team
                    {
                        Name = "Edmonton Oilfields",
                        LeagueCode = "ML"
                    }
                    ); ;
                }
                //Player Data
                if (!context.Players.Any())
                {
                    DateTime startDOB = DateTime.Today;
                    context.Players.AddRange(
                        new Player
                        {
                            FirstName = "Jim",
                            LastName = "Othy",
                            Jersey = "12",
                            DOB = startDOB.AddDays(-random.Next(6500, 25000)),
                            FeePaid = 125.00,
                            EMail = "jimothy@jimjim.com"
                        },
                        new Player
                        {
                            FirstName = "Antoine",
                            LastName = "Krealif",
                            Jersey = "88",
                            DOB = startDOB.AddDays(-random.Next(6500, 25000)),
                            FeePaid = 190.00,
                            EMail = "Antoine@email.com",
                        },
                        new Player
                        {
                            FirstName = "Nick",
                            LastName = "Olas",
                            Jersey = "29",
                            DOB = startDOB.AddDays(-random.Next(6500, 25000)),
                            FeePaid = 150.00,
                            EMail = "Nick@email.com"
                        },
                        new Player
                        {
                            FirstName = "Chris",
                            LastName = "Topher",
                            Jersey = "33",
                            DOB = startDOB.AddDays(-random.Next(6500, 25000)),
                            FeePaid = 150.00,
                            EMail = "Chris@email.com"
                        },
                        new Player
                        {
                            FirstName = "Jordan",
                            LastName = "Hunt",
                            Jersey = "99",
                            DOB = startDOB.AddDays(-random.Next(6500, 25000)),
                            FeePaid = 1000.00,
                            EMail = "Jordan@email.com"
                        },
                        new Player
                        {
                            FirstName = "Dale",
                            LastName = "Earnhardt",
                            Jersey = "3",
                            DOB = startDOB.AddDays(-random.Next(6500, 25000)),
                            FeePaid = 150.00,
                            EMail = "Dale@email.com"
                        },
                        new Player
                        {
                            FirstName = "Trevor",
                            LastName = "Zegras",
                            Jersey = "13",
                            DOB = startDOB.AddDays(-random.Next(6500, 25000)),
                            FeePaid = 150.00,
                            EMail = "Trevor@email.com"
                        },
                        new Player
                        {
                            FirstName = "Rick",
                            LastName = "Walking",
                            Jersey = "65",
                            DOB = startDOB.AddDays(-random.Next(6500, 25000)),
                            FeePaid = 150.00,
                            EMail = "Rick@email.com"
                        },
                        new Player
                        {
                            FirstName = "Jeremey",
                            LastName = "Krocker",
                            Jersey = "11",
                            DOB = startDOB.AddDays(-random.Next(6500, 25000)),
                            FeePaid = 200.00,
                            EMail = "Jeremey@email.com"
                        },
                        new Player
                        {
                            FirstName = "Sidney",
                            LastName = "Crosby",
                            Jersey = "87",
                            DOB = startDOB.AddDays(-random.Next(6500, 25000)),
                            FeePaid = 150.00,
                            EMail = "Sidney@email.com"
                        },
                        new Player
                        {
                            FirstName = "Connor",
                            LastName = "McDavid",
                            Jersey = "97",
                            DOB = startDOB.AddDays(-random.Next(6500, 25000)),
                            FeePaid = 150.00,
                            EMail = "Connor@email.com"
                        },
                        new Player
                        {
                            FirstName = "Alex",
                            LastName = "Jenners",
                            Jersey = "1",
                            DOB = startDOB.AddDays(-random.Next(6500, 25000)),
                            FeePaid = 150.00,
                            EMail = "Alex@email.com"
                        },
                        new Player
                        {
                            FirstName = "Peter",
                            LastName = "Kochetkov",
                            Jersey = "20",
                            DOB = startDOB.AddDays(-random.Next(6500, 25000)),
                            FeePaid = 150.00,
                            EMail = "Peter@email.com"
                        });
                }
                if (!context.TeamPlayers.Any())
                {
                    context.TeamPlayers.AddRange(
                        new TeamPlayer
                        {
                            PlayerID = 1,
                            TeamID = 2
                        },
                        new TeamPlayer
                        {
                            PlayerID = 1,
                            TeamID = 3
                        },
                        new TeamPlayer
                        {
                            PlayerID = 2,
                            TeamID = 1
                        }, 
                        new TeamPlayer
                        {
                            PlayerID = 2,
                            TeamID = 3
                        },
                        new TeamPlayer
                        {
                            PlayerID = 3,
                            TeamID = 1
                        },

                        new TeamPlayer
                        {
                            PlayerID = 4,
                            TeamID = 5
                        },

                        new TeamPlayer
                        {
                            PlayerID = 5,
                            TeamID = 6
                        },

                        new TeamPlayer
                        {
                            PlayerID = 6,
                            TeamID = 5
                        },

                        new TeamPlayer
                        {
                            PlayerID = 7,
                            TeamID = 4
                        },
                        new TeamPlayer
                        {
                            PlayerID = 8,
                            TeamID = 3
                        },

                        new TeamPlayer
                        {
                            PlayerID = 9,
                            TeamID = 2
                        },

                        new TeamPlayer
                        {
                            PlayerID = 10,
                            TeamID = 1
                        },

                        new TeamPlayer
                        {
                            PlayerID = 11,
                            TeamID = 0
                        },

                        new TeamPlayer
                        {
                            PlayerID = 10,
                            TeamID = 6
                        },

                        new TeamPlayer
                        {
                            PlayerID = 9,
                            TeamID = 4
                        });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
