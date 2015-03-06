using System.ComponentModel.DataAnnotations;

namespace Spreadbot.App.Web
{
    // >> | Model | DemoshopModel
    public class DemoshopModel
    {
        public static DemoshopItem StoredItem;

        static DemoshopModel()
        {
            StoredItem = new DemoshopItem()
            {
                Sku = "DS-001",
                Title = "Demoshop Single Item",
                Price = 115.00m,
                Quantity = 3
            };
        }

        public DemoshopItem Item
        {
            get { return StoredItem; }
        }

        public static void SaveItem(DemoshopItem item)
        {
            StoredItem = item;
        }
    }

    public class DemoshopItem
    {
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        public string Sku { get; set; }

        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = true)]
        public decimal Quantity { get; set; }
    }
}