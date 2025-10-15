using System;

namespace RegisterAppBenner.Models
{
    public class ProductModel
    {
        private static int _nextId = 1;
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public ProductModel( string name, string code, decimal price)
        {
            Id = _nextId++;

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

    }
}
