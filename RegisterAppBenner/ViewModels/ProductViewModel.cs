using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using RegisterAppBenner.Models;
using RegisterAppBenner.Services;

namespace RegisterAppBenner.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly ProductService _productService;
        private ProductModel? _selectedProduct;

        public ObservableCollection<ProductModel> Products { get; set; }
        public ProductModel? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        private string _name = string.Empty;
        private string _code = string.Empty;
        private decimal _price;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); } // Notify UI of changes 
        }
        public string Code
        {
            get => _code;
            set { _code = value; OnPropertyChanged(); } // Notify UI of changes
        }
        public decimal Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); } // Notify UI of changes
        }

        public ProductViewModel()
        {
            _productService = new ProductService();
            Products = new ObservableCollection<ProductModel>(_productService.GetAll());
        }

        public void AddProduct()
        {
            try
            {
                var product = new ProductModel(Name, Code, Price); // Create new product
                _productService.Add(product);  // Add to service (with validation)
                Products.Add(product);         // Add to observable collection for UI
                ClearFields();                 // Clear input fields
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao adicionar produto", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdateProduct()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Selecione um produto para atualizar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Price <= 0)
            {
                MessageBox.Show("Informe um preço válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _productService.UpdatePrice(SelectedProduct.Id, Price);
            SelectedProduct.Price = Price;
            MessageBox.Show("Produto atualizado com sucesso!");
        }

        public void DeleteProduct()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Selecione um produto para excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _productService.DeleteById(SelectedProduct.Id);
            Products.Remove(SelectedProduct);
            MessageBox.Show("Produto removido com sucesso!");
        }


        private void ClearFields()  // general function to clear input fields
        {
            Name = string.Empty;
            Code = string.Empty;
            Price = 0;
        }

        public event PropertyChangedEventHandler? PropertyChanged; 
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)  // Notify UI of property changes
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
