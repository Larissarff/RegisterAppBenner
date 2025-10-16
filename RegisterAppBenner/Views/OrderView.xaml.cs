using System.Windows;
using System.Windows.Controls;
using RegisterAppBenner.ViewModels;

namespace RegisterAppBenner.Views
{
    public partial class OrderView : UserControl 
    {
        private readonly OrderViewModel _viewModel;

        public OrderView()
        {
            InitializeComponent();
            _viewModel = (OrderViewModel)DataContext;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddItemToOrder();
        }

        private void FinalizeOrder_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.FinalizeOrder();
        }
    }
}
