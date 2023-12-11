using Microsoft.EntityFrameworkCore;
using UserDomain;

namespace Users.Persistence.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
                
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<VerificaUserSMS> UserSMS { get; set; }
    }
}