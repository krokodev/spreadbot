// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// YamlUtils.cs

using System.IO;
using YamlDotNet.Serialization;

namespace Spreadbot.Sdk.Common.Crocodev.Common
{
    public static class YamlUtils
    {
        public static string MakeYamlString( object obj, SerializationOptions options = SerializationOptions.None )
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

        public static string ToYamlString( this object obj, SerializationOptions options = SerializationOptions.None )
        {
            return MakeYamlString( obj, options );
        }

        public static string MakeArgsInfo( params object[] args )
        {
            return args.ToYamlString();
        }
    }
}