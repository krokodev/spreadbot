// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// YamlExtensions.cs
// romak_000, 2015-03-26 19:31

using System.IO;
using YamlDotNet.Serialization;

namespace Spreadbot.Sdk.Common.Crocodev.Common
{
    public static class YamlExtensions
    {
        // Code: YamlExtensions
        public static string ToYamlString( this object obj )
        {
            var serializer = new Serializer( SerializationOptions.None );
            var sw = new StringWriter();

            serializer.Serialize( sw, obj );

            return sw.ToString().UnescapeSlashes();
        }
    }
}