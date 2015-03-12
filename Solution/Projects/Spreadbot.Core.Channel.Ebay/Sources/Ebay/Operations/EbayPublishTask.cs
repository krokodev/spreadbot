using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Core.System;

namespace Spreadbot.Core.Channel.Ebay
{
    public sealed class EbayPublishTask : ChannelTask
    {
        // ===================================================================================== []
        // Constructor
        public EbayPublishTask(MipFeedType mipFeedType, string feedContent, string itemInfo)
            :base(ChannelMethod.Publish)
        {
            Channel = new EbayChannel();
            Args = new EbayPublishArgs
            {
                MipFeed = new MipFeed(mipFeedType)
                {
                    Content = feedContent,
                    ItemInfo = itemInfo
                }
            };
        }

        // ===================================================================================== []
        // Response
        public new ChannelResponse<EbayPublishResult> Response
        {
            get
            {
                return (ChannelResponse<EbayPublishResult>)base.Response; 
            }
        }

        // ===================================================================================== []
        // StatusCode
        public override TaskStatusCode StatusCode
        {
            get
            {
                if (Response==null)
                {
                    return TaskStatusCode.Todo;
                }
                return TaskStatusCode.Inprocess;
            }
        }

        // ===================================================================================== []
        // Autoinfo
        // Code: * EbayPublishTask : Autoinfo
        public override string Autoinfo
        {
            get
            {
                return string.Format(
                    "{0}, {1}, {2}, {3}, {4}, {5}",
                    StatusCode,
                    Id,
                    CreationTime.ToShortTimeString(),
                    IsCriticalInfo,
                    MissionInfo,
                    ResponseResultInfo
                    );
            }
        }

        // --------------------------------------------------------[]
        public object IsCriticalInfo
        {
            get { return IsCritical ? "Critical" : "Non critical"; }
        }

        // --------------------------------------------------------[]
        public string ResponseResultInfo
        {
            get
            {
                return "{0}".TryFormat(Response == null 
                    ? "no response" 
                    : Response.Result.ToString());
            }
        }

        // --------------------------------------------------------[]
        public string MissionInfo
        {
            get
            {
                var args = (EbayPublishArgs) Args;
                return "Publish [{0}, {1}] on eBay".TryFormat(args.MipFeed.Name, args.MipFeed.ItemInfo);
            }
        }
    }
}