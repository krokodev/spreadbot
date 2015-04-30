// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsSubmissionDescriptor.cs

using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Submission
{
    public class MwsSubmissionDescriptor
    {
        public MwsSubmissionDescriptor( MwsFeedDescriptor feedDescriptor, string requestId )
        {
            MwsFeedDescriptor = feedDescriptor;
            Id = requestId;
        }

        public string Id { get; set; }
        public MwsFeedDescriptor MwsFeedDescriptor { get; set; }
    }
}