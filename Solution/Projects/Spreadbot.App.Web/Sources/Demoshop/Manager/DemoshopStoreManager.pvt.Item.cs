// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopStore.pvt.Item.cs
// romak_000, 2015-03-19 17:15

using Nereal.Serialization;
using Spreadbot.App.Web.Sources.Demoshop.Item;

namespace Spreadbot.App.Web.Sources.Demoshop.Store
{
    public partial class DemoshopStoreManager
    {
        // ===================================================================================== []
        // Item
        [NotSerialize]
        public DemoshopItem Item { get; set; }

        // --------------------------------------------------------[]
        private void LoadItem()
        {
            Item = new DemoshopItem()
            {
                Sku = "DS-1001",
                Title = "Demoshop Single Item",
                Price = 7.00m,
                Quantity = 3
            };
        }

        // --------------------------------------------------------[]
        public void SaveItem(DemoshopItem item)
        {
            Item = item;
        }
    }
}