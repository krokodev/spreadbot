using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class FindRemoteFileResult : IResponseResult
    {
        public readonly string FileName;
        public readonly string FolderPath;

        public FindRemoteFileResult(string folderPath, string fileName)
        {
            FolderPath = folderPath;
            FileName = fileName;
        }

        public string GetAutoinfo(string format)
        {
            return format.SafeFormat("FolderPath", FolderPath) + " " + format.SafeFormat("FileName", FileName);
        }
    }
}