using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsLeagueDB.Core.Models
{
    public class SeasonStanding
    {
        [Key]
        public int StandingId { get; set; }

        public int SeasonYear { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Points { get; set; }

        [ForeignKey(nameof(SportsLeague))]
        public int LeagueId { get; set; }

        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }

        // Navigation properties
        public virtual SportsLeague SportsLeague { get; set; }
        public virtual Team Team { get; set; }
    }
}
