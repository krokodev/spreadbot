// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.pub.Mock.cs
// romak_000, 2015-03-24 12:12

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
        // Code: MipConnector.Mock_GetRequestStatus
        public static MipRequestStatusResponse Mock_GetRequestStatus( MipRequestHandler mipRequestHandler )
        {
            try {
                return Mock_GetRequestOutputStatus(
                    mipRequestHandler.MipFeedHandler.Type, 
                    new MipResponse< MipFindRemoteFileResult >(
                        true,
                        MipStatusCode.FindRequestSuccess,
                        new MipFindRemoteFileResult("mock", mipRequestHandler.FileNamePrefix()+".xml"))
                    );
            }
            catch( Exception exception ) {
                return new MipRequestStatusResponse( false, MipStatusCode.GetRequestStatusFail, exception );
            }
        }

        // --------------------------------------------------------[]
        private static MipRequestStatusResponse Mock_GetRequestOutputStatus(
            MipFeedType feedType,
            MipResponse< MipFindRemoteFileResult > response )
        {
            var statusResult = Mock_ReadRequestOutputStatus(feedType, response);
            return new MipRequestStatusResponse( true, MipStatusCode.GetRequestStatusSuccess, statusResult );
        }

        // --------------------------------------------------------[]
        private static MipGetRequestStatusResult Mock_ReadRequestOutputStatus(
            MipFeedType feedType,
            MipResponse< MipFindRemoteFileResult > response )
        {
            var fileName = response.Result.RemoteFileName;

            var localPath = string.Format(@"{0}\{1}", LocalRequestResultsFolder(), fileName);
            var content = File.ReadAllText(localPath);

            return MakeRequestStatusResultByParsingXmlContent(feedType, content);
        }
    }
}