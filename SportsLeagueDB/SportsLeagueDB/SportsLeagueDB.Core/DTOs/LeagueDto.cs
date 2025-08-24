namespace SportsLeagueDB.Core.DTOs
{
    public class LeagueDto
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string Country { get; set; }
        public int FoundedYear { get; set; }
        public int NumberOfTeams { get; set; }
    }
}
