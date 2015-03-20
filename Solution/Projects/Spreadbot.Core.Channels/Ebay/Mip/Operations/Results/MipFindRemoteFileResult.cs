// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipFindRemoteFileResult.cs
// romak_000, 2015-03-20 13:56

using Crocodev.Common.Extensions;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipFindRemoteFileResult : AbstractMipResponseResult
    {
        public readonly string FileName;
        public readonly string FolderPath;

        public MipFindRemoteFileResult( string folderPath, string fileName )
        {
            FolderPath = folderPath;
            FileName = fileName;
        }

        public override string Autoinfo
        {
            get
            {
                return Template.SafeFormat( "FolderPath", FolderPath ) + ", "
                       + Template.SafeFormat( "FileName", FileName );
            }
        }
    }
}