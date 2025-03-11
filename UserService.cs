using Microsoft.Extensions.Configuration;
using TestBlazorAppGJackson.Classes;

namespace TestBlazorAppGJackson.Services
{
    public class UserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public User GetUser()
        {
            // Correctly accessing the User under AppSettings section
            var user = _configuration.GetSection("AppSettings:User").Get<User>();
            return user;
        }
    }
}
