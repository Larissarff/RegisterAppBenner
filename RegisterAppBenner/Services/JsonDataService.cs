using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RegisterAppBenner.Services
{
    // Serviço genérico para operações CRUD em arquivos JSON
    public class JsonDataService<TypeOfModel>
    {
        private readonly string _filePath;

        public JsonDataService(string filePath)
        {
            _filePath = filePath;

            // Garante que a pasta exista
            var dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        // Ler todos os registros
        public List<TypeOfModel> LoadData()
        {
            if (!File.Exists(_filePath))
                return new List<TypeOfModel>();

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<TypeOfModel>>(json) ?? new List<TypeOfModel>();
        }

        // Salvar todos os registros
        public void SaveData(List<TypeOfModel> data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        // Adicionar um novo registro
        public void Add(TypeOfModel newItem)
        {
            var data = LoadData();
            data.Add(newItem);
            SaveData(data);
        }

        // Atualizar registros com base em uma condição
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

        // Excluir registros com base em uma condição
        public void Delete(Func<TypeOfModel, bool> condition)
        {
            var data = LoadData();
            data.RemoveAll(new Predicate<TypeOfModel>(condition));
            SaveData(data);
        }
    }
}
