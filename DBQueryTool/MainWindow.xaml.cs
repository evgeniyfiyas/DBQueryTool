using ClosedXML.Report;
using DBQueryTool.Models.DataProviders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DBQueryTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectionTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (MSAccessDataProvider.Connect(ConnectionStringTextBox.Text))
            {
                MessageBox.Show("Connected.", "DBQueryTool: Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ConnectionStringTextBox.IsEnabled = false;
                ConnectionTestButton.IsEnabled = false;
                QueryTextBox.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Invalid connection string.", "DBQueryTool: Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ConnectionStringTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConnectionTestButton.IsEnabled = ConnectionStringTextBox.Text.Length > 0 ? true : false;
        }

        private void QueryGoButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void LoadTemplateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportToXlsButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
