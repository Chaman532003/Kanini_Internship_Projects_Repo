using Microsoft.EntityFrameworkCore;
using SportsLeagueDB.Core.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SportsLeagueDB
{
    public class SportsLeagueDbContext : DbContext
    {
        public SportsLeagueDbContext(DbContextOptions<SportsLeagueDbContext> options) : base(options)
        {
        }

        public DbSet<SportsLeague> SportsLeagues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }
        public DbSet<SeasonStanding> SeasonStandings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserTeam composite key if preferred (optional):
            // modelBuilder.Entity<UserTeam>()
            //     .HasKey(ut => new { ut.UserId, ut.TeamId });

            // Configure relationships:

            modelBuilder.Entity<SportsLeague>()
                .HasMany(sl => sl.Teams)
                .WithOne(t => t.SportsLeague)
                .HasForeignKey(t => t.LeagueId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.SeasonStandings)
                .WithOne(ss => ss.Team)
                .HasForeignKey(ss => ss.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.UserTeams)
                .WithOne(ut => ut.Team)
                .HasForeignKey(ut => ut.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserTeams)
                .WithOne(ut => ut.User)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SeasonStanding>()
                .HasOne(ss => ss.SportsLeague)
                .WithMany()
                .HasForeignKey(ss => ss.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
