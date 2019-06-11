using ClosedXML.Report;
using DBQueryTool.Core.Formatters;
using DBQueryTool.Models.DataProviders;
using DBQueryTool.Views.Renderers;
using Microsoft.Win32;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace DBQueryTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // TODO: Rework this global variables
        private DataTable queried;
        private string templateFilePath;

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
            var reader = MSAccessDataProvider.Query(QueryTextBox.Text);

            if (reader != null)
            {
                // If query was ok loading results to datatable
                queried = new DataTable();
                queried.Load(reader);
                MessageBox.Show("Query OK", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void LoadTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() == true)
            {
                // TODO: Handle possible exception when file is removed after loading BUT before exporting to excel
                templateFilePath = openFileDialog.FileName;
                MessageBox.Show("Template loaded", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Information);
                ExportToXlsButton.IsEnabled = true;
            }
        }

        private void ExportToXlsButton_Click(object sender, RoutedEventArgs e)
        {
            MSAccessFormatter formatter = new MSAccessFormatter();
            var formatted = formatter.Format(queried);

            var template = new XLTemplate(templateFilePath);
            template.AddVariable("Users", formatted);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Microsoft Excel Spreadsheet (*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = "xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                string outputFilePath = saveFileDialog.FileName;

                ExcelRenderer renderer = new ExcelRenderer();
                renderer.Render(formatted, outputFilePath, template);

                MessageBox.Show("Report generated", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Information);
            }            
        }

        private void QueryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            QueryGoButton.IsEnabled = QueryTextBox.Text.Length > 0 ? true : false;
        }
    }
}
