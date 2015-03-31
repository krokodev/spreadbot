// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// YamlExtensions.cs
// Roman, 2015-03-30 2:43 PM

using System.IO;
using YamlDotNet.Serialization;

namespace Spreadbot.Sdk.Common.Crocodev.Common
{
    public static class YamlExtensions
    {
        public static string ToYamlString( this object obj, SerializationOptions options = SerializationOptions.None )
        {
            var serializer = new Serializer( options );
            var sw = new StringWriter();

            serializer.Serialize( sw, obj );

            return sw
                .ToString()
                .UnescapeSlashes()
                .Replace( "\r", "" )
                .Replace( "\n\n", "\n" )
                .Replace( ": >-\n", ":\n" )
                .Replace( ": >\n", ":\n" )
                ;
        }
    }
}