using Microsoft.Extensions.Configuration;

namespace DAL.Helper
{
    public class DBConfig
    {
        private readonly IConfigurationRoot _configuration;

        public string ConnectionString =>
        #if DEBUG 
            _configuration["SQLite"];
        #else
            _configuration["MySQL"]; 
        #endif

        public DBConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("DALConfig.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }
    }
}
