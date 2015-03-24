// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.pvt.RequestXml.cs
// romak_000, 2015-03-24 13:28

using System.Collections.Generic;
using System.Xml;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    // Code: ParseRequestXmlContent
    public partial class MipConnector
    {
        // --------------------------------------------------------[]
        private static MipGetRequestStatusResult MakeReqreuesStatusResultByParsingXmlContent(
            MipFeedType feedType,
            string content )
        {
            return new MipGetRequestStatusResult(
                IsContentSuccessfull( feedType, content )
                    ? MipRequestStatus.Success
                    : MipRequestStatus.Fail,
                content );
        }

        // --------------------------------------------------------[]
        private static bool IsContentSuccessfull( MipFeedType feedType, string content )
        {
            var statusNodePath = new Dictionary< MipFeedType, string > {
                { MipFeedType.Product, "/productResponse/responseMessage/response/status" }
            };

            if( !statusNodePath.ContainsKey( feedType ) ) {
                return false;
            }

            var xml = new XmlDocument {
                InnerXml = content
            };

            var statusNode = xml.SelectSingleNode( statusNodePath[ feedType ] );
            return statusNode != null && statusNode.InnerText == "SUCCESS";
        }
    }
}