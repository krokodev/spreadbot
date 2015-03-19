// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipRequest.cs
// romak_000, 2015-03-19 15:49

using System;
using Spreadbot.Core.Connectors.Ebay.Mip.Feed;

namespace Spreadbot.Core.Connectors.Ebay.Mip.Operations.Request
{
    public class MipRequest
    {
        public MipRequest(MipFeed mipFeed, Guid requestId)
        {
            MipFeed = mipFeed;
            Id = requestId;
        }

        public Guid Id { get; set; }
        public MipFeed MipFeed { get; set; }

        public static Guid GenerateId()
        {
            return Guid.NewGuid();
        }

        public static Guid GenerateTestId()
        {
            return new Guid();
        }

        public static bool VerifyRequestId(Guid requestId)
        {
            Guid guid;
            return Guid.TryParse(requestId.ToString(), out guid);
        }

        public string FileNamePrefix()
        {
            return string.Format("{0}.{1}", MipFeed.Name, Id);
        }
    }
}