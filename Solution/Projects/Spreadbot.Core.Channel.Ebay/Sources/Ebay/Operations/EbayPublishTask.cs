﻿using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Core.System;
using Spreadbot.Sdk.Common;

// !>> Core | EBay | EbayPublishTask
namespace Spreadbot.Core.Channel.Ebay
{
    public sealed class EbayPublishTask : ChannelTask
    {
        // ===================================================================================== []
        // Constructor
        public EbayPublishTask(MipFeedType mipFeedType, string feedContent, string itemInfo)
            : base(ChannelMethod.Publish)
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
            MipRequestStatusCode = MipRequestStatus.Unknown;
        }

        // ===================================================================================== []
        // Response
        public new ChannelResponse<EbayPublishResult> Response
        {
            get { return (ChannelResponse<EbayPublishResult>) base.Response; }
        }

        // ===================================================================================== []
        // StatusCode
        // Code: ** EbayPublishTask : StatusCode
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
                    case MipRequestStatus.Unknown:
                        return TaskStatus.Inprocess;

                    case MipRequestStatus.Fail:
                        return TaskStatus.Fail;

                    case MipRequestStatus.Inprocess:
                        return TaskStatus.Inprocess;

                    case MipRequestStatus.Success:
                        return TaskStatus.Inprocess;
                }
                throw new SpreadbotException("Wrong MipRequestStatusCode [{0}]", MipRequestStatusCode);
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
                    "{0}, {1}, {2}, {3}, {4}",
                    StatusCode,
                    IsCriticalInfo,
                    MissionInfo,
                    ResponseResultInfo,
                    MipRequestStatusInfo
                    );
            }
        }

        // --------------------------------------------------------[]
        public string MipRequestStatusInfo
        {
            get { return "MipRequestStatus: [{0}]".SafeFormat(MipRequestStatusCode); }
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