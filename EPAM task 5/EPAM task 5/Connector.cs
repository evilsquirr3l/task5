using System.IO;
using Microsoft.Extensions.Configuration;

namespace EPAM_task_5
{
    class Connector
    {
        public static readonly string connectionString;

        static Connector()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}