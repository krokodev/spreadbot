// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopItem.cs
// Roman, 2015-04-03 8:16 PM

using System.ComponentModel.DataAnnotations;

namespace Spreadbot.Core.Stores.Demoshop.Items
{
    public class DemoshopItem
    {
        [DisplayFormat( DataFormatString = "{0}", ApplyFormatInEditMode = true )]
        public string Sku { get; set; }

        [DisplayFormat( DataFormatString = "{0}", ApplyFormatInEditMode = true )]
        public string Title { get; set; }

        [DisplayFormat( DataFormatString = "{0:n2}", ApplyFormatInEditMode = true )]
        public decimal Price { get; set; }

        [DisplayFormat( DataFormatString = "{0:n0}", ApplyFormatInEditMode = true )]
        public decimal Quantity { get; set; }

        public override string ToString()
        {
            return string.Format( "{0} {1} {2} {3}", Sku, Title, Price, Quantity );
        }
    }
}