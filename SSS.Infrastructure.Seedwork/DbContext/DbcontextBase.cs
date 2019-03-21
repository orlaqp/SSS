using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SSS.Domain.Core.Student;

namespace SSS.Infrastructure.Seedwork.DbContext
{
    public class DbcontextBase : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IHostingEnvironment _env;

        public DbcontextBase(IHostingEnvironment env)
        {
            _env = env;
        }

        public DbSet<Student> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
