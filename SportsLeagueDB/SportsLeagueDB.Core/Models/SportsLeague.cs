using System.ComponentModel.DataAnnotations;

namespace SportsLeagueDB.Core.Models
{
    public class SportsLeague
    {
        [Key]
        public int LeagueId { get; set; }

        [Required]
        [MaxLength(30)]
        public string LeagueName { get; set; }

        [MaxLength(30)]
        public string Country { get; set; }

        public int FoundedYear { get; set; }

        public int NumberOfTeams { get; set; }

        // Navigation property for related teams
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
