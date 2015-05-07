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
        private static MipGetFeedSubmissionOverallStatusResult MakeSubmissionOverallStatusResultByParsingXmlContent(
            MipFeedType feedType,
            string content )
        {
            return new MipGetFeedSubmissionOverallStatusResult {
                Status =
                    ToOverallStatus( GetSubmissionCompleteStatusFromContent( feedType, content ) ),
                MipItemId = GetSubmissionItemIdFromContent( feedType, content ),
                Details = content,
            };
        }

        // --------------------------------------------------------[]
        private static MipFeedSubmissionOverallStatus ToOverallStatus( MipFeedSubmissionCompleteStatus completeStatus )
        {
            switch( completeStatus ) {
                case MipFeedSubmissionCompleteStatus.Unknown :
                    return MipFeedSubmissionOverallStatus.Unknown;
                case MipFeedSubmissionCompleteStatus.Failure :
                    return MipFeedSubmissionOverallStatus.Failure;
                case MipFeedSubmissionCompleteStatus.Success :
                    return MipFeedSubmissionOverallStatus.Success;
            }
            return MipFeedSubmissionOverallStatus.Unknown;
        }

        // --------------------------------------------------------[]
        private static MipFeedSubmissionCompleteStatus GetSubmissionCompleteStatusFromContent(
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
                    < MipFeedType, Func< XmlDocument, MipFeedSubmissionCompleteStatus, MipFeedSubmissionCompleteStatus >
                        > {
                            { MipFeedType.Product, ExtraCheckProductCompleteStatus },
                            { MipFeedType.Availability, ExtraCheckAvailabilityCompleteStatus },
                            { MipFeedType.Distribution, ExtraCheckDistributionCompleteStatus },
                        };

            try {
                var xml = new XmlDocument {
                    InnerXml = content
                };

                var statusNode = xml.SelectSingleNode( statusNodePath[ feedType ] );
                if( statusNode != null ) {
                    switch( statusNode.InnerText ) {
                        case "SUCCESS" :
                            return extraSubmissionStatusControl[ feedType ]( xml,
                                MipFeedSubmissionCompleteStatus.Success );
                        case "FAILURE" :
                            return MipFeedSubmissionCompleteStatus.Failure;
                    }
                }
            }
            catch {
                // ignored
            }

            return MipFeedSubmissionCompleteStatus.Unknown;
        }

        // --------------------------------------------------------[]
        private static MipFeedSubmissionCompleteStatus ExtraCheckDistributionCompleteStatus(
            XmlDocument xml,
            MipFeedSubmissionCompleteStatus overallStatus )
        {
            return overallStatus;
        }

        // --------------------------------------------------------[]
        private static MipFeedSubmissionCompleteStatus ExtraCheckAvailabilityCompleteStatus(
            XmlDocument xml,
            MipFeedSubmissionCompleteStatus overallStatus )
        {
            var errorIdNode = xml.SelectSingleNode( "/inventoryResponse/error/errorID" );
            if( errorIdNode != null ) {
                return MipFeedSubmissionCompleteStatus.Failure;
            }
            return overallStatus;
        }

        // --------------------------------------------------------[]
        private static MipFeedSubmissionCompleteStatus ExtraCheckProductCompleteStatus(
            XmlDocument xml,
            MipFeedSubmissionCompleteStatus status )
        {
            return status;
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