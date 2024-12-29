using System.Collections.Generic;
using StructureMap;
using UriDi.Models.Configuration;

namespace UriDi.Console.Configuration
{
    public class ApplicationRegistry
    {
        public static IContainer GetContainer(Dictionary<string, RegionConfiguration> configurations)
        {
            var registry = new Registry();
            foreach (var entry in configurations)
            {
                registry.IncludeRegistry(new RegionRegistry(entry.Key, entry.Value));
            }

            var container = new Container(registry);
            return container;
        }
    }
}
