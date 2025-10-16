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

            public void Add(ProductModel product) // Add new product with code uniqueness check
            {
                if (Exists(product.Code))
                    throw new System.Exception("Product code already exists.");
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

            public bool Exists(string code) // Check if code exists
            {
                var product = _dataService.LoadData();
                return product.Any(p => p.Code == code);
            }

            public void UpdatePrice(int id, decimal newPrice) // Update price by ID
            {
                _dataService.Update(p => p.Id == id, p => p.Price = newPrice);
            }

            public void DeleteById(int id) => _dataService.Delete(p => p.Id == id); // Delete by ID
        }

}

