// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipFindRemoteFileResult.cs
// romak_000, 2015-03-24 11:57

using Spreadbot.Core.Channels.Ebay.Mip.Feed;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipFindRemoteFileResult : AbstractMipResponseResult
    {
        public readonly string RemoteFileName;
        public readonly string RemoteFolderPath;

        public MipFindRemoteFileResult( string remoteFolderPath, string remoteFileName )
        {
            RemoteFolderPath = remoteFolderPath;
            RemoteFileName = remoteFileName;
        }

        public override string Autoinfo
        {
            get
            {
                return string.Format( Template, "FolderPath", RemoteFolderPath ) + ", "
                    + string.Format( Template, "FileName", RemoteFileName );
            }
        }
    }
}