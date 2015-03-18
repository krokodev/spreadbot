using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class MipFindRemoteFileResult : AbstractMipResponseResult
    {
        public readonly string FileName;
        public readonly string FolderPath;

        public MipFindRemoteFileResult(string folderPath, string fileName)
        {
            FolderPath = folderPath;
            FileName = fileName;
        }

        public override string Autoinfo
        {
            get
            {
                return Template.SafeFormat("FolderPath", FolderPath) + ", " + Template.SafeFormat("FileName", FileName);
            }
        }
    }
}