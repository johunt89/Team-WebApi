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
        public DbSet<TeamPlayer> TeamPlayers { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //unique constraint(s)

            modelBuilder.Entity<League>()
                .HasKey(i => i.Code);

            modelBuilder.Entity<TeamPlayer>()
                .HasKey(i => new { i.PlayerID, i.TeamID });

            //modelBuilder.Entity<League>()
            //    .HasIndex(i => i.Code)
            //    .IsUnique();

            modelBuilder.Entity<League>()
                .HasMany<Team>(i => i.Teams)
                .WithOne(i => i.League)
                .HasForeignKey(i => i.LeagueCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasMany<TeamPlayer>(i => i.TeamPlayers)
                .WithOne(i => i.Team)
                .HasForeignKey(i => i.TeamID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Player>()
                .HasMany<TeamPlayer>(i => i.TeamPlayers)
                .WithOne(i => i.Player)
                .HasForeignKey(i => i.PlayerID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
