
using ShopifyProducts.Core.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopifyProducts.Core.Implementations
{
    /// <summary>
    /// Line items refer to any service or product added to an order, along with the quantity and price that pertain to them.
    /// </summary>
    public class LineItem : Entity, ILineItem
    {
        public decimal Value
        {
            get
            {
                if (Product != null)
                    return Product.Value * Quantity;
                return 0;
            }
            set
            {
            }
        }      // Price of the item
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("Product")]
        public long ProductId { get; set; }

        [ForeignKey("Order")]        
        public long OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}