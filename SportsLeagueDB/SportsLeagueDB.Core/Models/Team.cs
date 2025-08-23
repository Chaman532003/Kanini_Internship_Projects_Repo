using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsLeagueDB.Core.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        [MaxLength(30)]
        public string TeamName { get; set; }

        [MaxLength(20)]
        public string City { get; set; }

        [ForeignKey(nameof(SportsLeague))]
        public int LeagueId { get; set; }

        public int TotalWins { get; set; }

        // Navigation properties
        public virtual SportsLeague SportsLeague { get; set; }
        public virtual ICollection<SeasonStanding> SeasonStandings { get; set; } = new List<SeasonStanding>();
        public virtual ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();
    }
}
