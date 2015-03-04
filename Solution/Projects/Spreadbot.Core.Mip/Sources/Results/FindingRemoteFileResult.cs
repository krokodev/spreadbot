using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public class FindingRemoteFileResult : IResponseResult
    {
        public readonly string FileName;
        public readonly string FolderPath;

        public FindingRemoteFileResult(string folderPath, string fileName)
        {
            FolderPath = folderPath;
            FileName = fileName;
        }

        public string GetDescription(string format)
        {
            return format.SafeFormat("FolderPath", FolderPath) + " " + format.SafeFormat("FileName", FileName);
        }
    }
}