using System.Collections.Generic;
using System.IO;
using System.Linq;
using RegisterAppBenner.Models;
using RegisterAppBenner.Services;

namespace RegisterAppBenner.Services
{
    public class PersonService
    {
        private readonly JsonDataService<PersonModel> _dataService;
        private readonly string _filePath = Path.Combine("persons.json");

        public PersonService()
        {
            _dataService = new JsonDataService<PersonModel>(_filePath);

            var existing = _dataService.LoadData(); // Load existing data to sync IDs
        }

        public List<PersonModel> GetAll() => _dataService.LoadData(); // Get all persons

        public void Add(PersonModel person) // Add new person with CPF uniqueness check
        {
            if (Exists(person.Cpf))
                throw new System.Exception("CPF already exists.");
            _dataService.Add(person);
        }

        public PersonModel? GetById(int id) // Search by ID
        {
            var people = _dataService.LoadData();
            return people.FirstOrDefault(p => p.Id == id);
        }

        public PersonModel? GetByCpf(string cpf) // Search by CPF
        {
            var people = _dataService.LoadData();
            return people.FirstOrDefault(p => p.Cpf == cpf);
        }

        public bool Exists(string cpf) // Check if CPF exists
        {
            var people = _dataService.LoadData();
            return people.Any(p => p.Cpf == cpf);
        }

        public void UpdateAddress(int id, string newAddress) // Update address by ID
        {
            _dataService.Update(p => p.Id == id, p => p.Address = newAddress);
        }
        public void Update(PersonModel updatedPerson) // Update person details
        {
            _dataService.Update(p => p.Id == updatedPerson.Id, p =>
            {
                p.Name = updatedPerson.Name;
                p.Cpf = updatedPerson.Cpf;
                p.Address = updatedPerson.Address;
            });
        }

        public void DeleteById(int id) => _dataService.Delete(p => p.Id == id); // Delete by ID
    }
}
