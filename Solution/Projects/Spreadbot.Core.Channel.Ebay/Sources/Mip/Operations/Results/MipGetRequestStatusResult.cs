﻿using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class MipGetRequestStatusResult : MipResponseResult
    {
        public readonly MipRequestStatus Status;
        public readonly string Details;

        public MipGetRequestStatusResult(MipRequestStatus status, string details = "")
        {
            Status = status;
            Details = details;
        }

        public override string Autoinfo
        {
            get { return Template.SafeFormat("Status", Status) + ", " + Template.SafeFormat("Details", Details); }
        }
    }
}