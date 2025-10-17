using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using RegisterAppBenner.Enums;
using RegisterAppBenner.Models;
using RegisterAppBenner.Services;

namespace RegisterAppBenner.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private readonly OrderService _orderService;
        private readonly ProductService _productService;
        private readonly PersonService _personService;

        private OrderModel? _selectedOrder;
        private ProductModel? _selectedProduct;
        private PersonModel? _selectedPerson;
        private PaymentMethodEnum _selectedPaymentMethod;
        private int _quantity;
        private decimal _totalValue;
        private bool _isFinalized;

        public ObservableCollection<OrderModel> Orders { get; set; }
        public ObservableCollection<ProductModel> Products { get; set; }
        public ObservableCollection<PersonModel> Persons { get; set; }
        public ObservableCollection<OrderItemModel> OrderItems { get; set; }
        public ObservableCollection<PaymentMethodEnum> PaymentMethods { get; set; }

        public OrderModel? SelectedOrder
        {
            get => _selectedOrder;
            set { _selectedOrder = value; OnPropertyChanged(); }
        }

        public ProductModel? SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        public PersonModel? SelectedPerson
        {
            get => _selectedPerson;
            set { _selectedPerson = value; OnPropertyChanged(); }
        }

        public PaymentMethodEnum SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set { _selectedPaymentMethod = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        public decimal TotalValue
        {
            get => _totalValue;
            set { _totalValue = value; OnPropertyChanged(); }
        }

        public bool IsFinalized
        {
            get => _isFinalized;
            set { _isFinalized = value; OnPropertyChanged(); }
        }

        public OrderViewModel()
        {
            _orderService = new OrderService();
            _productService = new ProductService();
            _personService = new PersonService();

            Orders = new ObservableCollection<OrderModel>(_orderService.GetAll());
            Products = new ObservableCollection<ProductModel>(_productService.GetAll());
            Persons = new ObservableCollection<PersonModel>(_personService.GetAll());
            OrderItems = new ObservableCollection<OrderItemModel>();

            PaymentMethods = new ObservableCollection<PaymentMethodEnum>(
                Enum.GetValues(typeof(PaymentMethodEnum)).Cast<PaymentMethodEnum>()
            );
        }

        public void AddItemToOrder()
        {
            try
            {
                if (IsFinalized)
                    throw new Exception("Pedido já finalizado. Não é possível adicionar itens.");

                if (SelectedProduct == null)
                    throw new Exception("Selecione um produto.");

                if (Quantity <= 0)
                    throw new Exception("Informe uma quantidade válida.");

                var item = new OrderItemModel(SelectedProduct, Quantity);
                OrderItems.Add(item);

                TotalValue += SelectedProduct.Price * Quantity;
                SelectedProduct = null;
                Quantity = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao adicionar item", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void FinalizeOrder()
        {
            try
            {
                if (SelectedPerson == null)
                    throw new Exception("Selecione um cliente.");

                if (OrderItems.Count == 0)
                    throw new Exception("Adicione pelo menos um item ao pedido.");

                List<OrderItemModel> items = new List<OrderItemModel>(OrderItems);
                OrderModel order = new OrderModel(SelectedPerson, items, SelectedPaymentMethod);

                _orderService.Add(order);
                Orders.Add(order);

                IsFinalized = true;
                MessageBox.Show("Pedido finalizado e salvo com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao finalizar pedido", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteOrder()
        {
            if (SelectedOrder == null)
            {
                MessageBox.Show("Selecione um pedido para excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _orderService.DeleteById(SelectedOrder.Id);
            Orders.Remove(SelectedOrder);
            MessageBox.Show("Pedido removido com sucesso!");
        }

        public void ClearFields()
        {
            SelectedPerson = null;
            SelectedProduct = null;
            SelectedPaymentMethod = PaymentMethodEnum.Cash;
            Quantity = 0;
            TotalValue = 0;
            OrderItems.Clear();
            IsFinalized = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
