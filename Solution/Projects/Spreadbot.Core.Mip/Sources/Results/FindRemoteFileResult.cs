namespace Spreadbot.Core.Mip
{
    public class FindRemoteFileResult
    {
        public readonly string FileName;
        public readonly string FolderPath;

        public FindRemoteFileResult(string folderPath, string fileName)
        {
            FolderPath = folderPath;
            FileName = fileName;
        }
    }
}
