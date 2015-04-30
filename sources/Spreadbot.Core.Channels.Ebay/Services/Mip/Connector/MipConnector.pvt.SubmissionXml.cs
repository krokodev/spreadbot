// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipConnector.pvt.SubmissionXml.cs

using System;
using System.Collections.Generic;
using System.Xml;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Submission;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Connector
{
    public partial class MipConnector
    {
        // --------------------------------------------------------[]
        private static MipGetSubmissionStatusResult MakeSubmissionStatusResultByParsingXmlContent(
            MipFeedType feedType,
            string content )
        {
            return new MipGetSubmissionStatusResult {
                MipSubmissionStatusCode = GetSubmissionStatusFromContent( feedType, content ),
                MipItemId = GetSubmissionItemIdFromContent( feedType, content ),
                Details = content,
            };
        }

        // --------------------------------------------------------[]
        private static MipSubmissionStatus GetSubmissionStatusFromContent( MipFeedType feedType, string content )
        {
            var statusNodePath = new Dictionary< MipFeedType, string > {
                { MipFeedType.Product, "/productResponse/status" },
                { MipFeedType.Availability, "/inventoryResponse/status" },
                { MipFeedType.Distribution, "/distributionResponse/status" },
            };
            var extraSubmissionStatusControl =
                new Dictionary< MipFeedType, Func< XmlDocument, MipSubmissionStatus, MipSubmissionStatus > > {
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
                            return extraSubmissionStatusControl[ feedType ]( xml, MipSubmissionStatus.Success );
                        case "FAILURE" :
                            return MipSubmissionStatus.Failure;
                    }
                }
            }
            catch {
                // ignored
            }

            return MipSubmissionStatus.Unknown;
        }

        // --------------------------------------------------------[]
        private static MipSubmissionStatus ExtraCheckDistributionStatus(
            XmlDocument xml,
            MipSubmissionStatus defStatus )
        {
            return defStatus;
        }

        // --------------------------------------------------------[]
        private static MipSubmissionStatus ExtraCheckAvailabilityStatus(
            XmlDocument xml,
            MipSubmissionStatus defStatus )
        {
            var errorIdNode = xml.SelectSingleNode( "/inventoryResponse/error/errorID" );
            if( errorIdNode != null ) {
                return MipSubmissionStatus.Failure;
            }
            return defStatus;
        }

        // --------------------------------------------------------[]
        private static MipSubmissionStatus ExtraCheckProductStatus( XmlDocument xml, MipSubmissionStatus defStatus )
        {
            return defStatus;
        }

        // --------------------------------------------------------[]
        private static string GetSubmissionItemIdFromContent( MipFeedType feedType, string content )
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