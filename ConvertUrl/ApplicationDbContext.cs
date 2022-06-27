using ConvertUrlRepository.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConvertUrlRepository
{
    public class ApplicationDbContext : DbContext 
    {
        public DbSet<UrlData> UrlData { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
