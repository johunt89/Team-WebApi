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
                            Name = "Premier League"
                        },
                        new League 
                        { 
                            Name = "Major League"
                        },
                        new League
                        {
                            Name = "Pro League"
                        },
                        new League
                        {
                            Name = "Little League"
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
                        Name = "Toronto Laughs"
                    },
                    new Team
                    {
                        Name = "Hamilton Steel"
                    },
                    new Team
                    {
                        Name = "Sudbury Nickles"
                    },
                    new Team
                    {
                        Name = "Winnipeg Moose"
                    },
                    new Team
                    {
                        Name = "Saskatoon Flatlands"
                    }
                    );
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
                            FeePaid = 100.00,
                            EMail = "jimothy@jimjim.com"
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
