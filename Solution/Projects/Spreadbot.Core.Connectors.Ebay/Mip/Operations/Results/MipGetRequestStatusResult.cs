// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipGetRequestStatusResult.cs
// romak_000, 2015-03-19 15:49

using Crocodev.Common.Extensions;
using Spreadbot.Core.Connectors.Ebay.Mip.Operations.Request;

namespace Spreadbot.Core.Connectors.Ebay.Mip.Operations.Results
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
            get
            {
                return Template.SafeFormat("Status", MipRequestStatusCode) + ", " +
                       Template.SafeFormat("Details", Details);
            }
        }
    }
}