using System;
using System.Collections.Generic;
using System.Linq;

namespace RegisterAppBenner.Models
{
    public class PersonModel
    {
        private static int _nextId = 1;
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public PersonModel(string name, string cpf, string address = "")
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));
            if (!IsValidCpf(cpf))
                throw new ArgumentException("Invalid CPF.", nameof(cpf));

            Id = _nextId++;
            Name = name.Trim();
            Cpf = cpf.Trim();
            Address = address.Trim();
        }

        private bool IsValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;
            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
                return false;

            return true; 
        }
        public static void SyncNextId(IEnumerable<PersonModel> existing)
        {
            if (existing.Any())
                _nextId = existing.Max(p => p.Id) + 1;
        }
    }
}
