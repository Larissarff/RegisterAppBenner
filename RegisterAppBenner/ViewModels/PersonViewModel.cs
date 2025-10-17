using RegisterAppBenner.Models;
using RegisterAppBenner.Services;
using RegisterAppBenner.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace RegisterAppBenner.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private readonly PersonService _personService;
        private readonly OrderService _orderService;

        public ObservableCollection<PersonModel> People { get; set; } = new();
        public ObservableCollection<OrderModel> PersonOrders { get; set; } = new();

        private PersonModel? _selectedPerson;
        public PersonModel? SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
                LoadOrdersForPerson();
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); ApplyFilters(); }
        }

        private string _cpf = string.Empty;
        public string Cpf
        {
            get => _cpf;
            set { _cpf = value; OnPropertyChanged(); ApplyFilters(); }
        }

        private string _address = string.Empty;
        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(); }
        }

        private string _nameFilter = string.Empty;
        public string NameFilter
        {
            get => _nameFilter;
            set { _nameFilter = value; OnPropertyChanged(); ApplyFilters(); }
        }

        private string _cpfFilter = string.Empty;
        public string CpfFilter
        {
            get => _cpfFilter;
            set { _cpfFilter = value; OnPropertyChanged(); ApplyFilters(); }
        }

        private string _statusOrderFilter = string.Empty;
        public string StatusOrderFilter
        {
            get => _statusOrderFilter;
            set
            {
                _statusOrderFilter = value;
                OnPropertyChanged();
                ApplyOrderStatusFilter();
            }
        }

        public PersonViewModel()
        {
            _personService = new PersonService();
            _orderService = new OrderService();
            LoadPeople();
        }

        public void AddPerson()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Cpf))
                    throw new Exception("Nome e CPF são obrigatórios.");

                PersonModel newPerson = new PersonModel(Name.Trim(), Cpf.Trim(), Address.Trim());
                _personService.Add(newPerson);
                People.Add(newPerson);
                ClearFields();

                MessageBox.Show("Pessoa adicionada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao adicionar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdatePerson()
        {
            if (SelectedPerson == null)
            {
                MessageBox.Show("Selecione uma pessoa para atualizar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _personService.Update(SelectedPerson.Id, Name, Cpf, Address);
                SelectedPerson.Name = Name;
                SelectedPerson.Cpf = Cpf;
                SelectedPerson.Address = Address;
                MessageBox.Show("Informações atualizadas com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeletePerson()
        {
            if (SelectedPerson == null)
            {
                MessageBox.Show("Selecione uma pessoa para excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _personService.DeleteById(SelectedPerson.Id);
                People.Remove(SelectedPerson);
                PersonOrders.Clear();
                MessageBox.Show("Pessoa excluída com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao excluir", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void LoadOrdersForPerson()
        {
            PersonOrders.Clear();
            if (SelectedPerson == null)
                return;

            var orders = _orderService.GetByCustomerCpf(SelectedPerson.Cpf);
            foreach (var order in orders)
                PersonOrders.Add(order);

            ApplyOrderStatusFilter();
        }

        public void OpenOrderWindow()
        {
            if (SelectedPerson == null)
            {
                MessageBox.Show("Selecione uma pessoa para incluir o pedido.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            OrderView orderView = new OrderView();

            OrderViewModel orderViewModel = (OrderViewModel)orderView.DataContext;
            orderViewModel.SelectedPerson = SelectedPerson;

            Window window = new Window
            {
                Title = $"Novo Pedido - {SelectedPerson.Name}",
                Content = orderView,
                Height = 650,
                Width = 950,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize
            };

            window.ShowDialog();
            LoadOrdersForPerson();
        }

        private void ApplyFilters()
        {
            List<PersonModel> filtered = _personService.GetAll()
                .Where(p =>
                    (string.IsNullOrWhiteSpace(NameFilter) ||
                     (!string.IsNullOrEmpty(p.Name) && p.Name.ToLower().Contains(NameFilter.ToLower()))) &&
                    (string.IsNullOrWhiteSpace(CpfFilter) ||
                     (!string.IsNullOrEmpty(p.Cpf) && p.Cpf.Contains(CpfFilter))))
                .ToList();

            People.Clear();
            foreach (PersonModel person in filtered)
                People.Add(person);
        }

        private void ApplyOrderStatusFilter()
        {
            if (SelectedPerson == null)
            {
                PersonOrders.Clear();
                return;
            }

            List<OrderModel> allOrders = _orderService.GetByCustomerCpf(SelectedPerson.Cpf);

            List<OrderModel> filteredOrders = new List<OrderModel>();
            foreach (OrderModel order in allOrders)
            {
                if (string.IsNullOrWhiteSpace(StatusOrderFilter) ||
                    order.Status.ToString().IndexOf(StatusOrderFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredOrders.Add(order);
                }
            }

            PersonOrders.Clear();
            foreach (OrderModel order in filteredOrders)
            {
                PersonOrders.Add(order);
            }
        }

        private void LoadPeople()
        {
            People.Clear();
            List<PersonModel> list = _personService.GetAll();
            foreach (PersonModel person in list)
                People.Add(person);
        }

        private void ClearFields()
        {
            Name = string.Empty;
            Cpf = string.Empty;
            Address = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
