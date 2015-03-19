// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// EbayChannel.pvt.Utils.cs
// romak_000, 2015-03-19 15:49

using System.IO;
using MoreLinq;
using Spreadbot.Core.Connectors.Ebay.Channel.Operations.Args;
using Spreadbot.Core.Connectors.Ebay.Channel.Operations.Tasks;
using Spreadbot.Core.Connectors.Ebay.Mip.Connector;
using Spreadbot.Core.Connectors.Ebay.Mip.Feed;
using Spreadbot.Core.Connectors.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Connectors.Ebay.Mip.Operations.Response;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Connectors.Ebay.Channel
{
    public partial class EbayChannel
    {
        // --------------------------------------------------------[]
        private static void CreateFeedFile(MipFeed mipFeed)
        {
            CreateFeedFolderIfNeed(mipFeed);

            var fileName = MipConnector.LocalFeedXmlFilePath(mipFeed);
            using (var file = File.CreateText(fileName))
            {
                file.Write(mipFeed.Content);
            }
        }

        // --------------------------------------------------------[]
        private static void CreateFeedFolderIfNeed(MipFeed mipFeed)
        {
            var feedFolder = MipConnector.LocalFeedFolder(mipFeed);
            if (!Directory.Exists(feedFolder))
            {
                Directory.CreateDirectory(feedFolder);
            }
        }

        // --------------------------------------------------------[]
        private static void EraseFeedFolder(MipFeed mipFeed)
        {
            Directory
                .GetFiles(MipConnector.LocalFeedFolder(mipFeed))
                .ForEach(File.Delete);
        }

        // --------------------------------------------------------[]
        private static void ProceedPublishTask(EbayPublishTask ebayPublishTask)
        {
            ((IProceedableTask) ebayPublishTask).AssertCanBeProceeded();

            MipRequestStatusResponse statusResponse = null;

            try
            {
                var ebayPublishArgs = (EbayPublishArgs) ebayPublishTask.GetChannelArgs();
                var mipRequest = new MipRequest(ebayPublishArgs.Feed, ebayPublishTask.GetMipRequestId());

                statusResponse = MipConnector.GetRequestStatus(mipRequest);
                statusResponse.Check();

                ebayPublishTask.MipRequestStatusCode = statusResponse.Result.MipRequestStatusCode;
                ((IProceedableTask) ebayPublishTask).SaveProceedInfo(statusResponse);
            }
            catch
            {
                ebayPublishTask.MipRequestStatusCode = MipRequestStatus.Fail;
                ((IProceedableTask) ebayPublishTask).SaveProceedInfo(statusResponse);
            }
        }
    }
}