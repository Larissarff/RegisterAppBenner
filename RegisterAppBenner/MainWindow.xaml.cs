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

                this.Loaded += (_, __) => // Testa o serviço JSON ao carregar a janela principal
                {
                try
                {
                    TestJsonDataService(); // Chama o método de teste
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
                string filePath = System.IO.Path.Combine("Data", "persons_test.json");


                var service = new JsonDataService<PersonModel>(filePath); // test add

                var person1 = new PersonModel("Larissa", "12345678901", "Rua A, 100");
                var person2 = new PersonModel("Tiago", "98765432100", "Rua B, 200");

                service.Add(person1);
                service.Add(person2);

                var people = service.LoadData(); // test load
                Console.WriteLine(" Pessoas cadastradas:");
                foreach (var p in people)
                    Console.WriteLine($"{p.Id} - {p.Name} - {p.Cpf} - {p.Address}");

                service.Update(p => p.Name == "Larissa", p => p.Address = "Avenida Nova, 500"); // test update

                service.Delete(p => p.Name == "Tiago"); // test delete

                var finalList = service.LoadData();

                Console.WriteLine("\n Lista final:"); // show final list
                foreach (var p in finalList)
                    Console.WriteLine($"{p.Id} - {p.Name} - {p.Cpf} - {p.Address}");
            }
        }
    
}
