// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipRequestHandler.cs
// romak_000, 2015-03-26 19:42

using System;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Request
{
    public class MipRequestHandler
    {
        public MipRequestHandler( MipFeedHandler mipFeedHandler, string requestId )
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