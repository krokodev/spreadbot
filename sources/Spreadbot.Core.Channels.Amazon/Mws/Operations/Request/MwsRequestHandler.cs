// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsRequestHandler.cs

using System;
using Spreadbot.Core.Channels.Amazon.Mws.Feed;

namespace Spreadbot.Core.Channels.Amazon.Mws.Operations.Request
{
    public class MwsRequestHandler
    {
        public MwsRequestHandler( MwsFeedHandler mwsFeedHandler, string requestId )
        {
            MwsFeedHandler = mwsFeedHandler;
            Id = requestId;
        }

        public string Id { get; set; }
        public MwsFeedHandler MwsFeedHandler { get; set; }

        public static string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool VerifyRequestId( string requestId )
        {
            Guid guid;
            return Guid.TryParse( requestId, out guid );
        }

        public string FileNamePrefix()
        {
            return string.Format( "{0}.{1}", MwsFeedHandler.GetName(), Id );
        }

        public static string GenerateZeroId()
        {
            return new Guid().ToString();
        }
    }
}