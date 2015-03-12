using System;
using Crocodev.Common.Identifier;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class MipRequest : Identifiable<MipRequest, Guid>
    {
        public MipRequest(MipFeed mipFeed, Identifier requestId)
        {
            MipFeed = mipFeed;
            Id = requestId;
        }

        public Identifier Id { get; set; }
        public MipFeed MipFeed { get; set; }

        public static Identifier GenerateId()
        {
            return (Identifier) Guid.NewGuid();
        }
        public static Identifier GenerateTestId()
        {
            return (Identifier)new Guid();
        }

        public static bool VerifyRequestId(Identifier requestId)
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