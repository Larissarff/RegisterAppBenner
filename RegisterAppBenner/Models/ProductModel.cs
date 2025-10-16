using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegisterAppBenner.Models
{
    public class ProductModel
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public ProductModel( string name, string code, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            if (!int.TryParse(code, out int parsedCode))
                throw new ArgumentException("Invalid Code.", nameof(code));

            if (price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));

            Name = name.Trim();
            Code = code.Trim();
            Price = price;
        }

        [JsonConstructor]
        private ProductModel(int id, string name, string code, decimal price)
        {
            Id = id;
            Name = name;
            Code = code;
            Price = price;
        }
    }
}
