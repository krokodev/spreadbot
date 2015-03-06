namespace Spreadbot.App.Web
{
    // >> | Model | DemoshopModel
    public class DemoshopModel
    {
        public static DemoshopItemModel StoredItem;

        static DemoshopModel()
        {
            StoredItem = new DemoshopItemModel()
            {
                Sku = "DS-001",
                Title = "Demoshop Single Item",
                Price = 115.00m,
                Quantity = 3
            };
        }

        public DemoshopItemModel Item
        {
            get { return StoredItem; }
        }

        public static void SaveItem(DemoshopItemModel item)
        {
            StoredItem = item;
        }
    }
}