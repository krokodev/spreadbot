﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipFeedSubmissionDescriptor.cs

using System;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission
{
    public class MipFeedSubmissionDescriptor
    {
        public string FeedSubmissionId { get; set; }
        public MipFeedDescriptor MipFeedDescriptor { get; set; }

        public MipFeedSubmissionDescriptor( MipFeedDescriptor mipFeedDescriptor, string feedSubmissionId )
        {
            MipFeedDescriptor = mipFeedDescriptor;
            FeedSubmissionId = feedSubmissionId;
        }

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
            return string.Format( "{0}.{1}", MipFeedDescriptor.GetName(), FeedSubmissionId );
        }

        public static string GenerateZeroId()
        {
            return new Guid().ToString();
        }
    }
}