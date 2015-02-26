using System.Configuration;
using System.Runtime.CompilerServices;

// Todo: Move to Nuget Package
namespace Crocodev.Common
{
    public class SmartConfigurationSection : ConfigurationSection
    {
        protected static string GetPropertyName([CallerMemberName] string name = "")
        {
            return name;
        }
    }
}