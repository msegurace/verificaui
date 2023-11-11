using Microsoft.EntityFrameworkCore;
using TokenDomain;

namespace Token.Persistence.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
                
        }

        public DbSet<Token2FA> Tokens { get; set; }
    }
}