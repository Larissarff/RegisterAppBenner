using System;

namespace RegisterAppBenner.Models
{
    public class OrderItemModel
    {
        public ProductModel Product { get; private set; }
        public int Quantity { get; private set; }

        public OrderItemModel(ProductModel product, int quantity)
        {
            if (product == null)
                throw new ArgumentException("Product is required.", nameof(product));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            Product = product;
            Quantity = quantity;
        }
    }
}
