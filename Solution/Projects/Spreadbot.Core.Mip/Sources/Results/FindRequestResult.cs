namespace Spreadbot.Core.Mip
{
    public class FindRequestResult
    {
        public readonly string RemoteFileName;
        public readonly string RemoteFileFolder;

        public FindRequestResult(string remoteDir, string name)
        {
            RemoteFileName = name;
            RemoteFileFolder = remoteDir;
        }
    }
}
