using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RegisterAppBenner.Services
{
    // Serviço genérico para operações CRUD em arquivos JSON
    public class JsonDataService<TypeOfModel>
    {
        private readonly string _filePath;

        public JsonDataService(string filePath)
        {
            _filePath = filePath;

            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public List<TypeOfModel> LoadData()
        {
            if (!File.Exists(_filePath))
                return new List<TypeOfModel>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<TypeOfModel>>(json) ?? new List<TypeOfModel>();
        }

        public void SaveData(List<TypeOfModel> data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public void Add(TypeOfModel item)
        {
            var data = LoadData();
            data.Add(item);
            SaveData(data);
        }

        public TypeOfModel? Get(Func<TypeOfModel, bool> predicate)
        {
            var data = LoadData();
            return data.FirstOrDefault(predicate);
        }

        public void Update(Func<TypeOfModel, bool> predicate, Action<TypeOfModel> updateAction)
        {
            var data = LoadData();
            var item = data.FirstOrDefault(predicate);

            if (item != null)
            {
                updateAction(item);
                SaveData(data);
            }
        }

        public void Delete(Func<TypeOfModel, bool> predicate)
        {
            var data = LoadData();
            var itemToRemove = data.FirstOrDefault(predicate);

            if (itemToRemove != null)
            {
                data.Remove(itemToRemove);
                SaveData(data);
            }
        }
    }
}
