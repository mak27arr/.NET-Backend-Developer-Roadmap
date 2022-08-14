using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.Helper
{
    public class AuthConfig
    {
        private readonly IConfigurationRoot _configuration;

        public bool ValidateIssuerSigningKey => bool.Parse(_configuration["JsonWebTokenKeys:ValidateIssuerSigningKey"]);
        public SymmetricSecurityKey IssuerSigningKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JsonWebTokenKeys:IssuerSigningKey"]));
        public bool ValidateIssuer => bool.Parse(_configuration["JsonWebTokenKeys:ValidateIssuer"]);
        public string ValidAudience => _configuration["JsonWebTokenKeys:ValidAudience"];
        public string ValidIssuer => _configuration["JsonWebTokenKeys:ValidIssuer"];
        public bool ValidateAudience => bool.Parse(_configuration["JsonWebTokenKeys:ValidateAudience"]);
        public bool RequireExpirationTime => bool.Parse(_configuration["JsonWebTokenKeys:RequireExpirationTime"]);
        public bool ValidateLifetime => bool.Parse(_configuration["JsonWebTokenKeys:ValidateLifetime"]);

        public AuthConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("AppConfig.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }
    }
}
