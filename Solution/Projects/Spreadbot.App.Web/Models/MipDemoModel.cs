using Crocodev.Common.Identifier;

namespace Spreadbot.App.Web
{
    // >> | Model | MipDemoModel
    public class MipDemoModel : Identifiable<MipDemoModel, int>
    {
        public MipDemoModel(Identifier id, string name)
        {
            Id = id;
            Name = name;
        }

        public Identifier Id { get; set; }

        public string Name { get; set; }
    }
}