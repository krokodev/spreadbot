// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipZipFeedResult.cs
// romak_000, 2015-03-21 2:11

using Crocodev.Common.Extensions;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipZipFeedResult : AbstractMipResponseResult
    {
        public MipZipFeedResult( string zipFileName )
        {
            ZipFileName = zipFileName;
        }

        public string ZipFileName { get; set; }

        public override string Autoinfo
        {
            get { return Template.SafeFormat( "ZipFileName", ZipFileName ); }
        }
    }
}