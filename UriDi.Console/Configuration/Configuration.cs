using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using UriDi.Models.Configuration;

namespace UriDi.Console.Configuration
{
    public class Configuration
    {
        public static Dictionary<string, RegionConfiguration> GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var configurations = new Dictionary<string, RegionConfiguration>();

            foreach (var region in Region.All)
            {
                var regionConfiguration = new RegionConfiguration();
                configuration.GetSection(region).Bind(regionConfiguration);
                configurations.Add(region, regionConfiguration);
            }

            return configurations;
        }
    }
}
