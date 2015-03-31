﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pvt.Item.cs
// Roman, 2015-03-31 1:26 PM

using Nereal.Serialization;
using Spreadbot.Core.Stores.Demoshop.Items;

namespace Spreadbot.Core.Stores.Demoshop.Manager
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
            Item = new DemoshopItem {
                Sku = "DS-1001",
                Title = "Demoshop Single Item",
                Price = 7.00m,
                Quantity = 3
            };
        }

        // --------------------------------------------------------[]
        public void SaveItem( DemoshopItem item )
        {
            Item = item;
        }
    }
}