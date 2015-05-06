// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// XmlUtils.cs

using System;
using System.Xml;

namespace Spreadbot.Sdk.Common.Krokodev.Common
{
    public static class XmlUtils
    {
        public static string GetXmlValue( this string content, string path )
        {
            var xml = new XmlDocument {
                InnerXml = content
            };

            var itemIdNode = xml.SelectSingleNode( path );

            return itemIdNode == null ? null : itemIdNode.InnerText;
        }

        public static int? GetXmlIntValue( this string content, string path )
        {
            var val = content.GetXmlValue( path );
            if( val == null ) {
                return null;
            }
            try {
                return Convert.ToInt32( val );
            }
            catch {
                return null;
            }
        }
    }
}