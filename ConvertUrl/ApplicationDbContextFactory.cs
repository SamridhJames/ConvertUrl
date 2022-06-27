using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConvertUrlRepository
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Trusted_Connection=True;MultipleActiveResultSets=true";

            var contactOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            contactOptionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(contactOptionsBuilder.Options);

        }
    }
}

