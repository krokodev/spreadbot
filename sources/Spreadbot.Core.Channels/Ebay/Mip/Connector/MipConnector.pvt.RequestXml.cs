// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.pvt.RequestXml.cs

using System;
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

        // --------------------------------------------------------[]
        private static MipRequestStatus GetRequestStatusFromContent( MipFeedType feedType, string content )
        {
            var statusNodePath = new Dictionary< MipFeedType, string > {
                { MipFeedType.Product, "/productResponse/status" },
                { MipFeedType.Availability, "/inventoryResponse/status" },
                { MipFeedType.Distribution, "/distributionResponse/status" },
            };
            var extraRequestStatusControl =
                new Dictionary< MipFeedType, Func< XmlDocument, MipRequestStatus, MipRequestStatus > > {
                    { MipFeedType.Product, ExtraCheckProductStatus },
                    { MipFeedType.Availability, ExtraCheckAvailabilityStatus },
                    { MipFeedType.Distribution, ExtraCheckDistributionStatus },
                };

            try {
                var xml = new XmlDocument {
                    InnerXml = content
                };

                var statusNode = xml.SelectSingleNode( statusNodePath[ feedType ] );
                if( statusNode != null ) {
                    switch( statusNode.InnerText ) {
                        case "SUCCESS" :
                            return extraRequestStatusControl[ feedType ]( xml, MipRequestStatus.Success );
                        case "FAILURE" :
                            return MipRequestStatus.Failure;
                    }
                }
            }
            catch {
                // ignored
            }

            return MipRequestStatus.Unknown;
        }

        // --------------------------------------------------------[]
        private static MipRequestStatus ExtraCheckDistributionStatus( XmlDocument xml, MipRequestStatus defStatus )
        {
            return defStatus;
        }

        // --------------------------------------------------------[]
        private static MipRequestStatus ExtraCheckAvailabilityStatus( XmlDocument xml, MipRequestStatus defStatus )
        {
            var errorIdNode = xml.SelectSingleNode( "/inventoryResponse/error/errorID" );
            if( errorIdNode != null ) {
                return MipRequestStatus.Failure;
            }
            return defStatus;
        }

        // --------------------------------------------------------[]
        private static MipRequestStatus ExtraCheckProductStatus( XmlDocument xml, MipRequestStatus defStatus )
        {
            return defStatus;
        }

        // --------------------------------------------------------[]
        private static string GetRequestItemIdFromContent( MipFeedType feedType, string content )
        {
            var itemIdPath = new Dictionary< MipFeedType, string > {
                { MipFeedType.Product, "/productResponse/responseMessage/response/itemID" },
                { MipFeedType.Distribution, "/distributionResponse/responseMessage/itemID" }
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