using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using RegisterAppBenner.Models;
using RegisterAppBenner.Services;
using System.Windows.Input;

namespace RegisterAppBenner.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private readonly PersonService _personService;

        public ObservableCollection<PersonModel> People { get; set; }

        private PersonModel _selectedPerson;
        public PersonModel SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        private string _cpf;
        public string Cpf
        {
            get => _cpf;
            set { _cpf = value; OnPropertyChanged(nameof(Cpf)); }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }

        public PersonViewModel()
        {
            _personService = new PersonService();
            var list = _personService.GetAll();
            People = new ObservableCollection<PersonModel>(list);
        }

        public void AddPerson()
        {
            try
            {
                var newPerson = new PersonModel(Name, Cpf, Address);
                _personService.Add(newPerson);
                People.Add(newPerson);
                ClearFields();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Erro ao adicionar: {ex.Message}");
            }
        }

        public void UpdatePerson()
        {
            if (SelectedPerson == null)
            {
                System.Windows.MessageBox.Show("Selecione uma pessoa para atualizar.");
                return;
            }

            try
            {
                _personService.UpdateAddress(SelectedPerson.Id, SelectedPerson.Address);
                System.Windows.MessageBox.Show("Endereço atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Erro ao atualizar: {ex.Message}");
            }
        }

        public void DeletePerson()
        {
            if (SelectedPerson == null)
            {
                System.Windows.MessageBox.Show("Selecione uma pessoa para excluir.");
                return;
            }

            try
            {
                _personService.DeleteById(SelectedPerson.Id);
                People.Remove(SelectedPerson);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Erro ao excluir: {ex.Message}");
            }
        }

        private void ClearFields()
        {
            Name = string.Empty;
            Cpf = string.Empty;
            Address = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
