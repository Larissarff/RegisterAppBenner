using Newtonsoft.Json;
using RegisterAppBenner.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace RegisterAppBenner.Models
{
	public class OrderModel
	{
		public int Id { get; private set; }
		public PersonModel Person { get; private set; }
		public List<OrderItemModel> Items { get; private set; } = new();
		public decimal TotalValue { get; private set; }
		public DateTime SaleDate { get; private set; }
		public PaymentMethod PaymentMethod { get; private set; }
		public OrderStatus Status { get; private set; }

		public OrderModel(PersonModel person, List<OrderItemModel> items, PaymentMethod paymentMethod)
		{
			if (person == null)
				throw new ArgumentException("Person is required.", nameof(person));

			if (items == null || !items.Any())
				throw new ArgumentException("At least one product is required.", nameof(items));

			if (!Enum.IsDefined(typeof(PaymentMethod), paymentMethod))
				throw new ArgumentException("Invalid payment method.", nameof(paymentMethod));

			Person = person;
			Items = items;
			PaymentMethod = paymentMethod;

			TotalValue = Items.Sum(i => i.Product.Price * i.Quantity);
			SaleDate = DateTime.Now;
			Status = OrderStatus.Pending;
		}

		public void UpdateStatus(OrderStatus newStatus) // Method to update order status
        {
			Status = newStatus;
		}

		public void UpdatePaymentMethod(PaymentMethod newPaymentMethod) // Method to update payment method
        {
			if (!Enum.IsDefined(typeof(PaymentMethod), newPaymentMethod))
				throw new ArgumentException("Invalid payment method.", nameof(newPaymentMethod));
			PaymentMethod = newPaymentMethod;
        }

        [JsonConstructor]
        private OrderModel(int id, PersonModel person, List<OrderItemModel> items, decimal totalValue, DateTime saleDate, PaymentMethod paymentMethod, OrderStatus status)
        {
            Id = id;
            Person = person;
            Items = items ?? new List<OrderItemModel>();
            TotalValue = totalValue;
            SaleDate = saleDate;
            PaymentMethod = paymentMethod;
            Status = status;
        }
    }
}
