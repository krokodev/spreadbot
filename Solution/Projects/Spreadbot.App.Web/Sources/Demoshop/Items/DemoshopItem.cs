// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopItem.cs
// romak_000, 2015-03-19 15:49

using System.ComponentModel.DataAnnotations;

namespace Spreadbot.App.Web.Sources.Demoshop.Item
{
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

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Sku, Title, Price, Quantity);
        }
    }
}