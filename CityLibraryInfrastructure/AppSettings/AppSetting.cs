using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryInfrastructure.AppSettings
{
    public class AppSetting
    {
        public TokenOptions TokenOptions { get; set; }

        public string DbConnectionString { get; set; }

        public RedisConnection RedisConnection { get; set; }
    }

    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }

        public int AccessTokenExpiration { get; set; } //set as hours
        public int RefreshTokenExpiration { get; set; } //set as hours

        public string SecurityKey { get; set; }
    }

    public class RedisConnection
    {
        public string ConnectionString { get; set; }

        public string InstanceName { get; set; }
    }
}
