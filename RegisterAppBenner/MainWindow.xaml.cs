using System;
using System.Windows;
using RegisterAppBenner.Models;
using RegisterAppBenner.Services;
using System.IO;
using System.Windows.Shapes;

namespace RegisterAppBenner
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    
        public partial class MainWindow : Window
        {
            public MainWindow()
            {
                InitializeComponent();

                this.Loaded += (_, __) =>
                {
                try
                {
                    TestJsonDataService();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro de teste JSON",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };
        }

            private void TestJsonDataService()
            {
            // Define o caminho do arquivo JSON na pasta Data
            string filePath = System.IO.Path.Combine("Data", "persons_test.json");


            // Cria o serviço para PersonModel
            var service = new JsonDataService<PersonModel>(filePath);

                // 1️⃣ Criar alguns registros
                var person1 = new PersonModel("Larissa", "12345678901", "Rua A, 100");
                var person2 = new PersonModel("Tiago", "98765432100", "Rua B, 200");

                service.Add(person1);
                service.Add(person2);

                // 2️⃣ Ler todos
                var people = service.LoadData();
                Console.WriteLine("👥 Pessoas cadastradas:");
                foreach (var p in people)
                    Console.WriteLine($"{p.Id} - {p.Name} - {p.Cpf} - {p.Address}");

                // 3️⃣ Atualizar endereço
                service.Update(p => p.Name == "Larissa", p => p.Address = "Avenida Nova, 500");

                // 4️⃣ Excluir um registro
                service.Delete(p => p.Name == "Tiago");

                // 5️⃣ Recarregar e mostrar resultado final
                var finalList = service.LoadData();
                Console.WriteLine("\n✅ Lista final:");
                foreach (var p in finalList)
                    Console.WriteLine($"{p.Id} - {p.Name} - {p.Cpf} - {p.Address}");
            }
        }
    
}
