namespace SportsLeagueDB.Core.DTOs
{
    public class SeasonStandingDto
    {
        public int StandingId { get; set; }
        public int SeasonYear { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Points { get; set; }
        public int LeagueId { get; set; }
        public int TeamId { get; set; }
    }
}
