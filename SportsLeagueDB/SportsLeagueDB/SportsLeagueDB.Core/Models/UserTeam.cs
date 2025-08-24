using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsLeagueDB.Core.Models
{
    public class UserTeam
    {
        [Key]
        public int UserTeamId { get; set; }  // Primary key for junction table

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }

        [MaxLength(50)]
        public string RoleInTeam { get; set; }

        public DateTime JoinDate { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Team Team { get; set; }
    }
}
