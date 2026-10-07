using System;
namespace CMS.BusinessLayer
{
    public class OrderItem
    {
        public OrderItem()
        {

        }

        public OrderItem(int orderItemId)
        {
            this.OrderItemId = orderItemId;
        }
        public int OrderItemId { get; private set; }
        public int OrderId { get; set; }
        public int OrderQuantity { get; set; }
        public int ProductId { get; set; }
        public decimal? PurchasePrice { get; set; }

        /// <summary>
        /// Validates the order item data.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            var valid = true;

            if (OrderQuantity <= 0) valid = false;
            if (ProductId <= 0) valid = false;
            if (PurchasePrice == null) valid = false;

            return valid;
        }
    }
}
