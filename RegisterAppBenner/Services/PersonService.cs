using System.Collections.Generic;
using System.IO;
using RegisterAppBenner.Models;
using RegisterAppBenner.Services;

namespace RegisterAppBenner.Services
{
    public class PersonService
    {
        private readonly JsonDataService<PersonModel> _dataService;
        private readonly string _filePath = Path.Combine("Data", "persons.json");

        public PersonService()
        {
            _dataService = new JsonDataService<PersonModel>(_filePath);
        }

        public List<PersonModel> GetAll() => _dataService.LoadData();

        public void Add(PersonModel person) => _dataService.Add(person);

        public PersonModel? GetById(int id) => _dataService.Get(p => p.Id == id);

        public void UpdateAddress(int id, string newAddress)
        {
            _dataService.Update(p => p.Id == id, p => p.Address = newAddress);
        }

        public void DeleteById(int id) => _dataService.Delete(p => p.Id == id);
    }
}
