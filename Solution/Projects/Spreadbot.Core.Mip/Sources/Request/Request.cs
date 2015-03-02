using System;
using Crocodev.Common.Identifier;

namespace Spreadbot.Core.Mip
{
    public class Request : Identifiable<Request, Guid>
    {
        public Request(Feed feed, Identifier requestId)
        {
            Feed = feed;
            Id = requestId;
        }

        public Identifier Id { get; set; }
        public Feed Feed { get; set; }

        public static Identifier GenerateId()
        {
            return (Identifier) Guid.NewGuid();
        }

        public static bool VerifyRequestId(Identifier requestId)
        {
            Guid guid;
            return Guid.TryParse(requestId.ToString(), out guid);
        }
    }
}