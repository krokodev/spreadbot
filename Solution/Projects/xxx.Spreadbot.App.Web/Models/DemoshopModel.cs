using System.ComponentModel.DataAnnotations;

namespace Spreadbot.App.Web
{
    // >> | Model | DemoshopModel
    public class DemoshopModel
    {
        public DemoshopModel()
        {
            Item = new Item()
            {
                Sku = "DS-001",
                Title = "Demoshop Single Item",
                Price = 115.00m,
                Quantity = 3
            };
        }

        public Item Item { get; set; }
    }

    public class Item
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