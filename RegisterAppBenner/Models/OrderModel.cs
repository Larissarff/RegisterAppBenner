using System;
using System.Collections.Generic;
using System.Linq;
using RegisterAppBenner.Enums;

namespace RegisterAppBenner.Models
{
	public class OrderModel
	{
		private static int _nextId = 1;

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

			Id = _nextId++;
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

		public void UpdadePaymentMethod(PaymentMethod newPaymentMethod) // Method to update payment method
        {
			if (!Enum.IsDefined(typeof(PaymentMethod), newPaymentMethod))
				throw new ArgumentException("Invalid payment method.", nameof(newPaymentMethod));
			PaymentMethod = newPaymentMethod;
        }

        public static void SyncNextId(IEnumerable<PersonModel> existing) // Sync next ID based on existing data
        {
            if (existing.Any())
                _nextId = existing.Max(p => p.Id) + 1;
        }
    }
}
