using System.Windows;
using System.Windows.Controls;
using RegisterAppBenner.ViewModels;

namespace RegisterAppBenner.Views
{
    public partial class PersonView : UserControl
    {
        private readonly PersonViewModel _viewModel;

        public PersonView()
        {
            InitializeComponent();
            _viewModel = (PersonViewModel)DataContext;
        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddPerson();
        }

        private void UpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdatePerson();
        }

        private void DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeletePerson();
        }
    }
}
