using ApplicationDomain;
using Microsoft.EntityFrameworkCore;

namespace Application.Persistence.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
                
        }

        public DbSet<Aplicacion> Aplicaciones { get; set; }
    }
}