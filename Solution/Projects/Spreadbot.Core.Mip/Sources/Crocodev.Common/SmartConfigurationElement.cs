using System.Configuration;
using System.Runtime.CompilerServices;

namespace Crocodev.Common
{
    public class SmartConfigurationElement : ConfigurationElement
    {
        protected static string GetPropertyName([CallerMemberName] string name = "")
        {
            return name;
        }
    }
}