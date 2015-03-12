using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class FindRemoteFileResult : ResponseResult
    {
        public readonly string FileName;
        public readonly string FolderPath;

        public FindRemoteFileResult(string folderPath, string fileName)
        {
            FolderPath = folderPath;
            FileName = fileName;
        }

        public override string GetAutoinfo()
        {
            return 
                Template.SafeFormat(
                    "FolderPath", FolderPath)
                   + " " +
                   Template.SafeFormat("FileName", FileName);
        }
    }
}