// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Stores
// DemoshopItem.cs

using System.ComponentModel.DataAnnotations;
using Nereal.Serialization;

namespace Spreadbot.Core.Stores.Demoshop.Items
{
    public class DemoshopItem
    {
        [Serialize]
        [DisplayFormat( DataFormatString = "{0}", ApplyFormatInEditMode = true )]
        public string Sku { get; set; }

        [Serialize]
        [DisplayFormat( DataFormatString = "{0}", ApplyFormatInEditMode = true )]
        public string Title { get; set; }

        [Serialize]
        [DisplayFormat( DataFormatString = "{0:n2}", ApplyFormatInEditMode = true )]
        public decimal Price { get; set; }

        [Serialize]
        [DisplayFormat( DataFormatString = "{0:n0}", ApplyFormatInEditMode = true )]
        public decimal Quantity { get; set; }

        public override string ToString()
        {
            return string.Format( "{0} {1} {2} {3}", Sku, Title, Price, Quantity );
        }
    }
}