
namespace REST_API
{
    public class AppConfigurationProvider : Persistence.IConfigurationProvider
    {

        private readonly IConfiguration _configuration;

        public AppConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string? GetConnectionString() => _configuration.GetConnectionString("DefaultConnection");

    }
}
