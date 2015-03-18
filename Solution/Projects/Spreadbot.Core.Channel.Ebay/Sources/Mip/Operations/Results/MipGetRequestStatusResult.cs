using Crocodev.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class MipGetRequestStatusResult : AbstractMipResponseResult
    {
        public readonly MipRequestStatus MipRequestStatusCode;
        public readonly string Details;

        public MipGetRequestStatusResult(MipRequestStatus mipRequestStatusCode, string details = "")
        {
            MipRequestStatusCode = mipRequestStatusCode;
            Details = details;
        }

        public override string Autoinfo
        {
            get { return Template.SafeFormat("Status", MipRequestStatusCode) + ", " + Template.SafeFormat("Details", Details); }
        }
    }
}