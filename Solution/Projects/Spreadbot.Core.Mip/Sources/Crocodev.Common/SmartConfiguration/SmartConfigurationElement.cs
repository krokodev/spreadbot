using System.Configuration;
using System.Runtime.CompilerServices;

namespace Crocodev.Common.SmartConfiguration
{
    public class SmartConfigurationElement : ConfigurationElement
    {
        protected static string GetPropertyName([CallerMemberName] string name = "")
        {
            return name;
        }
    }
}