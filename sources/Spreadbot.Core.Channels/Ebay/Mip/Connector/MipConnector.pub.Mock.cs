// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.pub.Mock.cs
// Roman, 2015-04-03 1:45 PM

using System;
using System.IO;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    // Ref: Use Mock<MipConnector> + regular method GetRequestStatus
    public partial class MipConnector
    {
        public static MipRequestStatusResponse Mock_GetRequestStatus( MipRequestHandler mipRequestHandler )
        {
            try {
                return Mock_GetRequestOutputStatus(
                    mipRequestHandler.MipFeedHandler.Type,
                    new MipResponse< MipFindRemoteFileResult > {
                        StatusCode = MipOperationStatus.FindRequestSuccess,
                        Result = new MipFindRemoteFileResult {
                            RemoteDir = "mock",
                            RemoteFileName = mipRequestHandler.FileNamePrefix() + ".xml"
                        }
                    } );
            }
            catch( Exception exception ) {
                return new MipRequestStatusResponse( exception ) {
                    StatusCode = MipOperationStatus.GetRequestStatusFailure
                };
            }
        }

        // --------------------------------------------------------[]
        private static MipRequestStatusResponse Mock_GetRequestOutputStatus(
            MipFeedType feedType,
            MipResponse< MipFindRemoteFileResult > response )
        {
            return new MipRequestStatusResponse {
                StatusCode = MipOperationStatus.GetRequestStatusSuccess,
                Result = Mock_ReadRequestOutputStatus( feedType, response )
            };
        }

        // --------------------------------------------------------[]
        private static MipGetRequestStatusResult Mock_ReadRequestOutputStatus(
            MipFeedType feedType,
            MipResponse< MipFindRemoteFileResult > response )
        {
            var fileName = response.Result.RemoteFileName;

            var localPath = string.Format( @"{0}\{1}", LocalRequestResultsFolder(), fileName );
            var content = File.ReadAllText( localPath );

            return MakeRequestStatusResultByParsingXmlContent( feedType, content );
        }
    }
}