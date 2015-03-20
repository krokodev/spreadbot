// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipGetRequestStatusResult.cs
// romak_000, 2015-03-20 13:56

using Crocodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipGetRequestStatusResult : AbstractMipResponseResult
    {
        public readonly MipRequestStatus MipRequestStatusCode;
        public readonly string Details;

        public MipGetRequestStatusResult( MipRequestStatus mipRequestStatusCode, string details = "" )
        {
            MipRequestStatusCode = mipRequestStatusCode;
            Details = details;
        }

        public override string Autoinfo
        {
            get
            {
                return Template.SafeFormat( "Status", MipRequestStatusCode ) + ", " +
                       Template.SafeFormat( "Details", Details );
            }
        }
    }
}