using System;

namespace SportsLeagueDB.Core.DTOs
{
    public class UserTeamDto
    {
        public int UserTeamId { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public string RoleInTeam { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
