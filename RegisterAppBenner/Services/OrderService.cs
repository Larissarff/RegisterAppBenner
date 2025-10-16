using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RegisterAppBenner.Enums;
using RegisterAppBenner.Models;

namespace RegisterAppBenner.Services
{
    public class OrderService
    {
        private readonly JsonDataService<OrderModel> _dataService;
        private readonly string _filePath = Path.Combine("Data", "orders.json");

        public OrderService()
        {
            _dataService = new JsonDataService<OrderModel>(_filePath);

            var existing = _dataService.LoadData();
            OrderModel.SyncNextId(existing);
        }
        public List<OrderModel> GetAll() => _dataService.LoadData(); // Get all orders

        public OrderModel? GetById(int id) // Search by ID
        {
            var orders = _dataService.LoadData();
            return orders.FirstOrDefault(o => o.Id == id);
        }
        public List<OrderModel> GetByCustomerCpf(string cpf) // Search by Customer CPF
        {
            var orders = _dataService.LoadData();
            return orders.Where(o => o.Person.Cpf == cpf).ToList();
        }

        public List<OrderModel> GetByStatus(OrderStatus status) // Search by Order Status
        {
            var orders = _dataService.LoadData();
            return orders.Where(o => o.Status == status).ToList();
        }

        public void Add(OrderModel order) // Add new order
        {
            if (order == null)
                throw new ArgumentException("Order cannot be null.", nameof(order));

            _dataService.Add(order);
        }

        public void UpdateStatus(int orderId, OrderStatus newStatus) // Update order status
        {
            _dataService.Update(o => o.Id == orderId, o => o.UpdateStatus(newStatus));
        }
        
        public void UpdatePaymentMethod(int orderId, PaymentMethod newPaymentMethod) // Update payment method
        {
            _dataService.Update(o => o.Id == orderId, o => o.UpdatePaymentMethod(newPaymentMethod));
        }

        public void DeleteById(int id)  // Delete by ID
        {
            _dataService.Delete(o => o.Id == id);
        }

        public decimal GetTotalSales(DateTime start, DateTime end)
        {
            var orders = _dataService.LoadData();
            return orders
                .Where(o => o.SaleDate >= start && o.SaleDate <= end)
                .Sum(o => o.TotalValue);
        }
    }
}
