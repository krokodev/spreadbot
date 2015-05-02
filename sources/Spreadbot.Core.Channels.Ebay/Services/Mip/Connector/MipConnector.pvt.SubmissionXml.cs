// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipConnector.pvt.SubmissionXml.cs

using System;
using System.Collections.Generic;
using System.Xml;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Results;

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
                MipFeedSubmissionResultStatusCode = GetSubmissionStatusFromContent( feedType, content ),
                MipItemId = GetSubmissionItemIdFromContent( feedType, content ),
                Details = content,
            };
        }

        // --------------------------------------------------------[]
        private static MipFeedSubmissionResultStatus GetSubmissionStatusFromContent(
            MipFeedType feedType,
            string content )
        {
            var statusNodePath = new Dictionary< MipFeedType, string > {
                { MipFeedType.Product, "/productResponse/status" },
                { MipFeedType.Availability, "/inventoryResponse/status" },
                { MipFeedType.Distribution, "/distributionResponse/status" },
            };
            var extraSubmissionStatusControl =
                new Dictionary
                    < MipFeedType, Func< XmlDocument, MipFeedSubmissionResultStatus, MipFeedSubmissionResultStatus > > {
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
                            return extraSubmissionStatusControl[ feedType ]( xml, MipFeedSubmissionResultStatus.Success );
                        case "FAILURE" :
                            return MipFeedSubmissionResultStatus.Failure;
                    }
                }
            }
            catch {
                // ignored
            }

            return MipFeedSubmissionResultStatus.Unknown;
        }

        // --------------------------------------------------------[]
        private static MipFeedSubmissionResultStatus ExtraCheckDistributionStatus(
            XmlDocument xml,
            MipFeedSubmissionResultStatus defResultStatus )
        {
            return defResultStatus;
        }

        // --------------------------------------------------------[]
        private static MipFeedSubmissionResultStatus ExtraCheckAvailabilityStatus(
            XmlDocument xml,
            MipFeedSubmissionResultStatus defResultStatus )
        {
            var errorIdNode = xml.SelectSingleNode( "/inventoryResponse/error/errorID" );
            if( errorIdNode != null ) {
                return MipFeedSubmissionResultStatus.Failure;
            }
            return defResultStatus;
        }

        // --------------------------------------------------------[]
        private static MipFeedSubmissionResultStatus ExtraCheckProductStatus(
            XmlDocument xml,
            MipFeedSubmissionResultStatus defResultStatus )
        {
            return defResultStatus;
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