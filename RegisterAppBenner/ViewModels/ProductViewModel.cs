using RegisterAppBenner.Models;
using RegisterAppBenner.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Linq;
using System;

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
        private string _nameFilter = string.Empty;
        private string _codeFilter = string.Empty;
        private decimal _minPriceFilter;
        private decimal _maxPriceFilter;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string Code
        {
            get => _code;
            set { _code = value; OnPropertyChanged(); }
        }

        public decimal Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); }
        }

        public string NameFilter
        {
            get => _nameFilter;
            set { _nameFilter = value; OnPropertyChanged(); ApplyFilters(); }
        }

        public string CodeFilter
        {
            get => _codeFilter;
            set { _codeFilter = value; OnPropertyChanged(); ApplyFilters(); }
        }

        public decimal MinPriceFilter
        {
            get => _minPriceFilter;
            set { _minPriceFilter = value; OnPropertyChanged(); ApplyFilters(); }
        }

        public decimal MaxPriceFilter
        {
            get => _maxPriceFilter;
            set { _maxPriceFilter = value; OnPropertyChanged(); ApplyFilters(); }
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
                ProductModel product = new ProductModel(Name, Code, Price); // Create new product
                _productService.Add(product);  // Add to service (with validation)
                Products.Add(product);         // Add to observable collection for UI
                ClearFields();                 // Clear input fields
                MessageBox.Show("Produto adicionado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
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

            _productService.UpdateProduct(SelectedProduct.Id,Name, Code, Price);
            SelectedProduct.Price = Price;
            SelectedProduct.Name = Name;
            SelectedProduct.Code = Code;
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

        private void ApplyFilters() 
        {
            List<ProductModel> allProducts = _productService.GetAll();

            bool NameMatches(ProductModel p)
                => string.IsNullOrWhiteSpace(NameFilter)
                   || (!string.IsNullOrEmpty(p.Name)
                       && p.Name.IndexOf(NameFilter, StringComparison.OrdinalIgnoreCase) >= 0);

            bool CodeMatches(ProductModel p)
                => string.IsNullOrWhiteSpace(CodeFilter)
                   || (!string.IsNullOrEmpty(p.Code)
                       && p.Code.IndexOf(CodeFilter, StringComparison.OrdinalIgnoreCase) >= 0);

            bool PriceMatches(ProductModel p)
                => (MinPriceFilter <= 0 || p.Price >= MinPriceFilter)
                   && (MaxPriceFilter <= 0 || p.Price <= MaxPriceFilter);

            IEnumerable<ProductModel> filtered = allProducts.Where(p =>
                NameMatches(p) && CodeMatches(p) && PriceMatches(p));

            Products.Clear();
            foreach (ProductModel product in filtered)
                Products.Add(product);
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
