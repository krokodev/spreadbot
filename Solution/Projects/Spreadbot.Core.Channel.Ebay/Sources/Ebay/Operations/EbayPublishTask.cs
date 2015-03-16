using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Sdk.Common;

// !>> Core | EBay | EbayPublishTask

namespace Spreadbot.Core.Channel.Ebay
{
    public sealed class EbayPublishTask : AbstractChannelTask, IProceedableTask
    {
        // ===================================================================================== []
        // Constructor
        public EbayPublishTask()
        {
            
        }
        public EbayPublishTask(MipFeedType mipFeedType, string feedContent, string itemInfo)
            : base(ChannelMethod.Publish)
        {
            Channel = new EbayChannel();
            Args = new EbayPublishArgs
            {
                Feed = new MipFeed(mipFeedType)
                {
                    Content = feedContent,
                    ItemInfo = itemInfo
                }
            };
            MipRequestStatusCode = MipRequestStatus.Unknown;
        }

        // ===================================================================================== []
        // Response
        public new ChannelResponse<EbayPublishResult> Response
        {
            get { return (ChannelResponse<EbayPublishResult>) base.Response; }
            set { base.Response = value; }
        }

        // ===================================================================================== []
        // StatusCode
        public MipRequestStatus MipRequestStatusCode { get; set; }
        // --------------------------------------------------------[]
        public override TaskStatus StatusCode
        {
            get
            {
                if (Response == null)
                {
                    return TaskStatus.Todo;
                }
                if (!Response.IsSuccess)
                {
                    return TaskStatus.Fail;
                }
                switch (MipRequestStatusCode)
                {
                    case MipRequestStatus.Initial:
                        return TaskStatus.Inprocess;

                    case MipRequestStatus.Inprocess:
                        return TaskStatus.Inprocess;

                    case MipRequestStatus.Unknown:
                        return TaskStatus.Fail;

                    case MipRequestStatus.Fail:
                        return TaskStatus.Fail;

                    case MipRequestStatus.Success:
                        return TaskStatus.Success;
                }
                throw new SpreadbotException("Wrong MipRequestStatusCode [{0}]", MipRequestStatusCode);
            }
        }

        // ===================================================================================== []
        // Autoinfo
        public override string Autoinfo
        {
            get
            {
                return string.Format(
                    "{3} {0}: {1} {2}",
                    StatusCode,
                    IsCriticalInfo,
                    MissionInfo,
                    Channel.Name
                    );
            }
        }

        // --------------------------------------------------------[]
        private string MipRequestStatusInfo
        {
            get { return "MipRequestStatus: [{0}]".SafeFormat(MipRequestStatusCode); }
        }

        // --------------------------------------------------------[]
        private string IsCriticalInfo
        {
            get { return IsCritical ? "Critical" : "Non critical"; }
        }

        // --------------------------------------------------------[]
        private string ResponseResultInfo
        {
            get
            {
                return "{0}".TryFormat(Response == null
                    ? "no response"
                    : Response.Result.ToString());
            }
        }

        // --------------------------------------------------------[]
        private string MissionInfo
        {
            get
            {
                var args = (EbayPublishArgs) Args;
                return "Publish [{0}]".TryFormat(args.Feed.Name);
            }
        }

        // ===================================================================================== []
        // IProceedableTask
        private readonly TaskProceedHelper _taskProceedHelper = new TaskProceedHelper();
        // --------------------------------------------------------[]
        public void SaveProceedInfo(ITaskProceedInfo info)
        {
            _taskProceedHelper.Save(info);
        }

        // --------------------------------------------------------[]
        public void AssertCanBeProceeded()
        {
            if (MipRequestStatusCode != MipRequestStatus.Initial &&
                MipRequestStatusCode != MipRequestStatus.Inprocess)
            {
                throw new SpreadbotException("Unexpected Task MipRequestStatusCode: [{0}]", MipRequestStatusCode);
            }
        }
    }
}