using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Data
{
    public class ProjectContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectContext(DbContextOptions<ProjectContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            if(_httpContextAccessor.HttpContext == null)
            {

            }
            else
            {

            }
        }

        #region DbSets

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }

        #endregion

        protected void OnModelCreating(ModelBuilder modelBuilder)  //override ??
        {

            //unique constraint(s)

            modelBuilder.Entity<League>()
                .HasIndex(i => i.Code)
                .IsUnique();

            modelBuilder.Entity<League>()
                .HasMany<Team>(i => i.Teams)
                .WithOne(i => i.League)
                .HasForeignKey(i => i.LeagueCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasMany<Player>(i => i.Players)
                .WithMany(i => i.Teams);
                //.OnDelete(DeleteBehavior.Restrict);
        }
    }
}
