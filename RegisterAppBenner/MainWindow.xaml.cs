using System.Windows;
using RegisterAppBenner.Views;

namespace RegisterAppBenner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            MainFrame.Navigate(new PersonView());
        }

        private void OpenPersonView_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PersonView());
        }

        private void OpenProductView_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProductView());
        }

        private void OpenOrderView_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new OrderView();
        }

        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
