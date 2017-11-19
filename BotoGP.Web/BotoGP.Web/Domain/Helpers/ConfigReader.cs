
using Microsoft.Extensions.Configuration;

namespace BotoGP.Domain.Helpers
{
    public static class ConfigReader
    {
        public static string ReadAppSetting(IConfiguration configuration, string name)
        {
            return configuration["APPSETTING_" + name] ?? configuration["AppSettings:" + name];
        }
    }
}
