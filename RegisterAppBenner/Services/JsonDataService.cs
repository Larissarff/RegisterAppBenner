using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace RegisterAppBenner.Services
{
    // Serviço genérico para operações CRUD em arquivos JSON
    public class JsonDataService<TypeOfModel>
    {
        private readonly string _filePath;

        public JsonDataService(string fileName)
        {
            var baseDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));

            string dataDir = Path.Combine(baseDir, "Data");

            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
                Console.WriteLine($"[INFO] Pasta criada: {dataDir}");
            }

            _filePath = Path.Combine(dataDir, fileName);

            Console.WriteLine($"[DEBUG] Caminho do arquivo JSON: {_filePath}");
        }


        public List<TypeOfModel> LoadData()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<TypeOfModel>();

                var json = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<TypeOfModel>>(json) ?? new List<TypeOfModel>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<TypeOfModel>();
            }
        }

        public void SaveData(List<TypeOfModel> data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(_filePath, json);
                Console.WriteLine($"[DEBUG] Dados salvos em {_filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar dados: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Add(TypeOfModel newItem)
        {
            var data = LoadData();

            var idProperty = typeof(TypeOfModel).GetProperty("Id");
            if (idProperty != null)
            {
                int lastId = data
                    .Select(item => (int)(idProperty.GetValue(item) ?? 0))
                    .DefaultIfEmpty(0)
                    .Max();

                int nextId = lastId + 1;
                idProperty.SetValue(newItem, nextId);
            }

            data.Add(newItem);
            SaveData(data);
        }

        public void Update(Func<TypeOfModel, bool> condition, Action<TypeOfModel> updateAction)
        {
            var data = LoadData();
            foreach (var item in data)
            {
                if (condition(item))
                    updateAction(item);
            }
            SaveData(data);
        }

        public void Delete(Func<TypeOfModel, bool> condition)
        {
            var data = LoadData();
            data.RemoveAll(new Predicate<TypeOfModel>(condition));
            SaveData(data);
        }
    }
}
