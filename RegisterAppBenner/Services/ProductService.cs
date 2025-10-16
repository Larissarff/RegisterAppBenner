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
            private readonly string _filePath = Path.Combine("Data", "product.json");

            public ProductService()
            {
                _dataService = new JsonDataService<ProductModel>(_filePath);

                var existing = _dataService.LoadData(); // Load existing data to sync IDs
                ProductModel.SyncNextId(existing);
            }

            public List<ProductModel> GetAll() => _dataService.LoadData(); // Get all products

            public void Add(ProductModel product) // Add new product with code and name uniqueness check
            {
                if (ExistsCode(product.Code))
                    throw new System.Exception("Product code already exists.");
                if (ExistsName(product.Name))
                    throw new System.Exception("Product name already exists.");

            _dataService.Add(product);
            }

            public ProductModel? GetById(int id) // Search by ID
            {
                var product = _dataService.LoadData();
                return product.FirstOrDefault(p => p.Id == id);
            }

            public ProductModel? GetByCode(string code) // Search by Code
            {
                var product = _dataService.LoadData();
                return product.FirstOrDefault(p => p.Code == code);
            }

            public ProductModel? GetByName(string name) // Search by Name
            {
                var product = _dataService.LoadData();
                return product.FirstOrDefault(p => p.Name == name);
            }

            public List<ProductModel> GetByRangeValue(decimal minValue, decimal maxValue) // Buscar por intervalo de valores
            {
                var products = _dataService.LoadData();
                return products.Where(p => p.Price >= minValue && p.Price <= maxValue).ToList();
            }

            public bool ExistsCode(string code) // Check if code exists
            {
                var product = _dataService.LoadData();
                return product.Any(p => p.Code == code);
            }

            public bool ExistsName(string name) // Check if name exists
            {
                var product = _dataService.LoadData();
                return product.Any(p => p.Name == name);
            }

            public void UpdatePrice(int id, decimal newPrice) // Update price by ID
            {
                _dataService.Update(p => p.Id == id, p => p.Price = newPrice);
            }

            public void DeleteById(int id) => _dataService.Delete(p => p.Id == id); // Delete by ID
        }

}

