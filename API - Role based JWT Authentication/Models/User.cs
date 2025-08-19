using System.ComponentModel.DataAnnotations;

namespace GeneratingTokens.Models
{
    public class User
    {
        [Key]
        public int? user_id { get; set; }
        public string? user_name { get; set; }
        public string? user_email { get; set; }
        public string? user_password { get; set; }
        public string? user_role { get; set; }
    }
}
