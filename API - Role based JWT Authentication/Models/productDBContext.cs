using Microsoft.EntityFrameworkCore;

namespace GeneratingTokens.Models
{
    public class productDBContext: DbContext
    {
     
        public productDBContext(DbContextOptions<productDBContext> options) : base(options)
        {
           
        }
        public DbSet<User> Users { get; set; }
        public DbSet<product> Products { get; set; }

    }
}
