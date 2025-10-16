using System.Windows;
using System.Windows.Controls;
using RegisterAppBenner.ViewModels;

namespace RegisterAppBenner.Views
{
    public partial class ProductView : UserControl
    {
        private readonly ProductViewModel _viewModel;

        public ProductView()
        {
            InitializeComponent();
            _viewModel = (ProductViewModel)DataContext;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddProduct();
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdateProduct();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteProduct();
        }
    }
}
