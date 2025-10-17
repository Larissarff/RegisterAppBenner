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
        private readonly string _filePath = Path.Combine("orders.json");

        public OrderService()
        {
            _dataService = new JsonDataService<OrderModel>(_filePath);

            List<OrderModel> existing = _dataService.LoadData();
        }
        public List<OrderModel> GetAll() => _dataService.LoadData(); // Get all orders

        public OrderModel? GetById(int id) // Search by ID
        {
            List<OrderModel> orders = _dataService.LoadData();
            return orders.FirstOrDefault(o => o.Id == id);
        }
        public List<OrderModel> GetByCustomerCpf(string cpf) // Search by Customer CPF
        {
            List<OrderModel> orders = _dataService.LoadData();
            return orders.Where(o => o.Person.Cpf == cpf).ToList();
        }

        public List<OrderModel> GetByStatus(OrderStatusEnum status) // Search by Order Status
        {
            List<OrderModel> orders = _dataService.LoadData();
            return orders.Where(o => o.Status == status).ToList();
        }

        public void Add(OrderModel order) // Add new order
        {
            if (order == null)
                throw new ArgumentException("Order cannot be null.", nameof(order));

            _dataService.Add(order);
        }

        public void UpdateStatus(int orderId, OrderStatusEnum newStatus) // Update order status
        {
            _dataService.Update(o => o.Id == orderId, o => o.UpdateStatus(newStatus));
        }
        
        public void UpdatePaymentMethod(int orderId, PaymentMethodEnum newPaymentMethod) // Update payment method
        {
            _dataService.Update(o => o.Id == orderId, o => o.UpdatePaymentMethod(newPaymentMethod));
        }

        public void DeleteById(int id)  // Delete by ID
        {
            _dataService.Delete(o => o.Id == id);
        }

        public decimal GetTotalSales(DateTime start, DateTime end)
        {
            List<OrderModel> orders = _dataService.LoadData();
            return orders
                .Where(o => o.SaleDate >= start && o.SaleDate <= end)
                .Sum(o => o.TotalValue);
        }
    }
}
