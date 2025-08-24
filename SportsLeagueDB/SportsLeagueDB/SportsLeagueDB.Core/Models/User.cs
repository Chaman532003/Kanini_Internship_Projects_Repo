using System.ComponentModel.DataAnnotations;

namespace SportsLeagueDB.Core.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [MaxLength(20)]
        public string Role { get; set; }

        // Navigation property
        public virtual ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();
    }
}
