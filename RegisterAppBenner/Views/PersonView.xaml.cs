using RegisterAppBenner.Enums;
using RegisterAppBenner.Models;
using RegisterAppBenner.Services;
using RegisterAppBenner.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace RegisterAppBenner.Views
{
    public partial class PersonView : UserControl
    {
        private readonly PersonViewModel _viewModel;

        public PersonView()
        {
            InitializeComponent();
            _viewModel = (PersonViewModel)DataContext;
        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddPerson();
        }

        private void UpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdatePerson();
        }
        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Row.Item is PersonModel editedPerson)
            {
                _viewModel.SelectedPerson = editedPerson;
            }
        }

        private void DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeletePerson();
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenOrderWindow();
        }

        private void MarkAsPaid_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPerson == null) return;
            if (sender is Button button && button.DataContext is OrderModel order)
                new OrderService().UpdateStatus(order.Id, OrderStatusEnum.Paid);
            _viewModel.LoadOrdersForPerson();
        }

        private void MarkAsShipped_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPerson == null) return;
            if (sender is Button button && button.DataContext is OrderModel order)
                new OrderService().UpdateStatus(order.Id, OrderStatusEnum.Shipped);
            _viewModel.LoadOrdersForPerson();
        }

        private void MarkAsDelivered_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPerson == null) return;
            if (sender is Button button && button.DataContext is OrderModel order)
                new OrderService().UpdateStatus(order.Id, OrderStatusEnum.Delivered);
            _viewModel.LoadOrdersForPerson();
        }
    }
}
