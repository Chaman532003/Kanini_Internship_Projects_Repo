namespace SportsLeagueDB.Core.DTOs
{
    public class TeamDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string City { get; set; }
        public int LeagueId { get; set; }
        public int TotalWins { get; set; }
    }
}
