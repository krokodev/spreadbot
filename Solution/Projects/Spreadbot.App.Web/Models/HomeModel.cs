using System.Linq;

namespace Spreadbot.App.Web
{
    // >> | Model | HomeModel
    public class HomeModel
    {
        public static readonly MipDemoModel[] MipDemoModels =
        {
            new MipDemoModel((MipDemoModel.Identifier)1, "Mip Demo #1"),
            new MipDemoModel((MipDemoModel.Identifier)2, "Mip Demo #2"),
        };

        public static MipDemoModel FindMipDemo(MipDemoModel.Identifier id)
        {
            return MipDemoModels.First(m => m.Id == id);
        }
    }
}