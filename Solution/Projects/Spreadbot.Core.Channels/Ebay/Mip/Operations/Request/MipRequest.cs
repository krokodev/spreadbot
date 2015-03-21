// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipRequest.cs
// romak_000, 2015-03-21 2:11

using System;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Request
{
    public class MipRequest
    {
        public MipRequest( MipFeedHandler mipFeedHandler, string requestId )
        {
            MipFeedHandler = mipFeedHandler;
            Id = requestId;
        }

        public string Id { get; set; }
        public MipFeedHandler MipFeedHandler { get; set; }

        public static string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        public static string GenerateTestId()
        {
            return new Guid().ToString();
        }

        public static bool VerifyRequestId( string requestId )
        {
            Guid guid;
            return Guid.TryParse( requestId, out guid );
        }

        public string FileNamePrefix()
        {
            return string.Format( "{0}.{1}", MipFeedHandler.GetName(), Id );
        }
    }
}