using Project1.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

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
                //League Data
                if (!context.Leagues.Any())
                {
                    context.Leagues.AddRange(
                        new League
                        {
                            Name = "Premier League"
                        });
                    context.SaveChanges();
                }
                //Team Data

                //Player Data
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
