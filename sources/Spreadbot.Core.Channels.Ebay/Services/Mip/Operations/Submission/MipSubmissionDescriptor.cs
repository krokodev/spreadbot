// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipSubmissionDescriptor.cs

using System;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Submission
{
    public class MipSubmissionDescriptor
    {
        public MipSubmissionDescriptor( MipFeedDescriptor mipFeedDescriptor, string submissionId )
        {
            MipFeedDescriptor = mipFeedDescriptor;
            Id = submissionId;
        }

        public string Id { get; set; }
        public MipFeedDescriptor MipFeedDescriptor { get; set; }

        public static string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool VerifySubmissionId( string submissionId )
        {
            Guid guid;
            return Guid.TryParse( submissionId, out guid );
        }

        public string FileNamePrefix()
        {
            return string.Format( "{0}.{1}", MipFeedDescriptor.GetName(), Id );
        }

        public static string GenerateZeroId()
        {
            return new Guid().ToString();
        }
    }
}