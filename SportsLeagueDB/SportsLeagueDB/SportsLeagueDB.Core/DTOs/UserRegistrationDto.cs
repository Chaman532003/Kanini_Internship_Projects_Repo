namespace SportsLeagueDB.Core.DTOs
{
    public class UserRegistrationDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; } // Accept the password via POST
    }
}
