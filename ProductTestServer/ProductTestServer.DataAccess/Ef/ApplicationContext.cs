using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using ProductTestServer.Common.Models;

namespace ProductTestServer.DataAccess.Ef
{
    public class ApplicationContext: DbContext
    {
        public IConfiguration Configuration { get; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
            CreateDatabaseIfNotExist();
        }


        private void CreateDatabaseIfNotExist()
        {
            var creator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

            var isExistsDb = creator.Exists();

            if (!isExistsDb)
            {
                creator.Create();
            }
        }

        public DbSet<Product> Products { get; set; }
    }
}
