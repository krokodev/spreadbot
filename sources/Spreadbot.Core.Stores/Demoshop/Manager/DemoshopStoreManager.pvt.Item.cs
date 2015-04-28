// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pvt.Item.cs

using Spreadbot.Core.Stores.Demoshop.Items;

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
    public partial class DemoshopStoreManager
    {
        public void SetItemToDefault()
        {
            Item = new DemoshopItem {
                Sku = "DS-1001",
                Title = "Demoshop Single Item",
                Price = 7.00m,
                Quantity = 3
            };
        }

        public void UpdateItem( DemoshopItem item )
        {
            Item = item;
            Message = "Item updated";
        }
    }
}