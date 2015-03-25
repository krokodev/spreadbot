// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.pvt.RequestXml.cs
// romak_000, 2015-03-25 13:41

using System.Collections.Generic;
using System.Xml;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    public partial class MipConnector
    {
        // --------------------------------------------------------[]
        private static MipGetRequestStatusResult MakeRequestStatusResultByParsingXmlContent(
            MipFeedType feedType,
            string content )
        {
            return new MipGetRequestStatusResult {
                MipRequestStatusCode = GetRequestStatusFromContent( feedType, content ),
                MipItemId = GetRequestItemIdFromContent( feedType, content ),
                Details = content,
            };
        }

        // Code: GetRequestStatusFromContent

        // --------------------------------------------------------[]
        private static MipRequestStatus GetRequestStatusFromContent( MipFeedType feedType, string content )
        {
            var statusNodePath = new Dictionary< MipFeedType, string > {
                { MipFeedType.Product, "/productResponse/status" },
                { MipFeedType.Availability, "/inventoryResponse/status" },
            };

            if( !statusNodePath.ContainsKey( feedType ) ) {
                return MipRequestStatus.Unknown;
            }

            var xml = new XmlDocument {
                InnerXml = content
            };
            var statusNode = xml.SelectSingleNode( statusNodePath[ feedType ] );

            if( statusNode == null ) {
                return MipRequestStatus.Unknown;
            }

            switch( statusNode.InnerText ) {
                case "SUCCESS" :
                    return MipRequestStatus.Success;
                case "FAILURE" :
                    return MipRequestStatus.Failure;
            }

            return MipRequestStatus.Unknown;
        }

        // --------------------------------------------------------[]
        private static string GetRequestItemIdFromContent( MipFeedType feedType, string content )
        {
            var itemIdPath = new Dictionary< MipFeedType, string > {
                { MipFeedType.Product, "/productResponse/responseMessage/response/itemID" }
            };

            if( !itemIdPath.ContainsKey( feedType ) ) {
                return null;
            }

            var xml = new XmlDocument {
                InnerXml = content
            };

            var itemIdNode = xml.SelectSingleNode( itemIdPath[ feedType ] );
            if( itemIdNode != null ) {
                return itemIdNode.InnerText;
            }

            return null;
        }
    }
}