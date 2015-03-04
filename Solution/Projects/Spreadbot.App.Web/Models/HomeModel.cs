namespace Spreadbot.App.Web
{
    // >> | Model | HomeModel
    public class HomeModel
    {
        public readonly MipDemoModel[] MipDemoModels =
        {
            new MipDemoModel((MipDemoModel.Identifier)1, "MipDemoModels #1"),
            new MipDemoModel((MipDemoModel.Identifier)2, "MipDemoModels #2"),
        };
    }
}