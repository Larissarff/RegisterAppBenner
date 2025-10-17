using RegisterAppBenner.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RegisterAppBenner.Services
{
    public class ProductService
    {
            private readonly JsonDataService<ProductModel> _dataService;
            private readonly string _filePath = Path.Combine("product.json");

            public ProductService()
            {
                _dataService = new JsonDataService<ProductModel>(_filePath);

                List<ProductModel> existing = _dataService.LoadData(); // Load existing data to sync IDs
            }

            public List<ProductModel> GetAll() => _dataService.LoadData(); // Get all products

            public void Add(ProductModel product) // Add new product with code and name uniqueness check
            {
                if (ExistsCode(product.Code))
                    throw new System.Exception("Product code already exists.");
                if (ExistsName(product.Name))
                    throw new System.Exception("Product name already exists.");
                if (product.Price == 0)
                    throw new System.Exception("Product price is required.");

            _dataService.Add(product);
            }

            public ProductModel? GetById(int id) // Search by ID
            {
                List<ProductModel> product = _dataService.LoadData();
                return product.FirstOrDefault(p => p.Id == id);
            }

            public ProductModel? GetByCode(string code) // Search by Code
            {
                List<ProductModel> product = _dataService.LoadData();
                return product.FirstOrDefault(p => p.Code == code);
            }

            public ProductModel? GetByName(string name) // Search by Name
            {
                List<ProductModel> product = _dataService.LoadData();
                return product.FirstOrDefault(p => p.Name == name);
            }

            public List<ProductModel> GetByRangeValue(decimal minValue, decimal maxValue) // Buscar por intervalo de valores
            {
                List<ProductModel> products = _dataService.LoadData();
                return products.Where(p => p.Price >= minValue && p.Price <= maxValue).ToList();
            }

            public bool ExistsCode(string code) // Check if code exists
            {
                List<ProductModel> product = _dataService.LoadData();
                return product.Any(p => p.Code == code);
            }

            public bool ExistsName(string name) // Check if name exists
            {
                List<ProductModel> product = _dataService.LoadData();
                return product.Any(p => p.Name == name);
            }

            public void UpdateProduct(int id, string newName, string newCode, decimal newPrice) // Update price by ID
            {
                List<ProductModel> product = _dataService.LoadData();
                ProductModel? existing = product.FirstOrDefault(p => p.Id == id);

                if (existing == null)
                    throw new Exception("Produto não encontrada.");

                _dataService.Update(p => p.Id == id,
                    p =>
                    {
                        p.Name = newName;
                        p.Code = newCode;
                        p.Price = newPrice;
                    });
            }

            public void DeleteById(int id)
            {
                _dataService.Delete(p => p.Id == id);

                var existing = _dataService.LoadData();
            }
    }

}

