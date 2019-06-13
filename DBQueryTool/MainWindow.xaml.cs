﻿using ClosedXML.Report;
using DBQueryTool.Core;
using DBQueryTool.Core.Formatters;
using DBQueryTool.Models.DataProviders;
using DBQueryTool.Views.Renderers;
using Microsoft.Win32;
using NLog;
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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private DataTable _queried;
        private string _templateFilePath;
        private MSAccessDataProvider _dataProvider;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectionTestButton_Click(object sender, RoutedEventArgs e)
        {
            var connectionString = ConnectionStringTextBox.Text;
            _dataProvider = new MSAccessDataProvider(connectionString);
            if (_dataProvider.TestConnection())
            {
                MessageBox.Show("Connected.", "DBQueryTool: Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ChangeControlsState(false, false, true, false, true, false);
            }
            else
            {
                MessageBox.Show("Invalid connection string.", "DBQueryTool: Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ConnectionStringTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConnectionTestButton.IsEnabled = ConnectionStringTextBox.Text.Length > 0;
        }

        private void QueryGoButton_Click(object sender, RoutedEventArgs e)
        {
            _queried = _dataProvider.Query(QueryTextBox.Text);

            logger.Info("Database query success");
            MessageBox.Show("Query OK", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "xlsx files (*.xlsx)|*.xlsx"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                logger.Info("XLSX template loaded successfully.");
                _templateFilePath = openFileDialog.FileName;
                MessageBox.Show("Template loaded", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Information);
                ExportToXlsButton.IsEnabled = true;
            }
        }

        private void ExportToXlsButton_Click(object sender, RoutedEventArgs e)
        {
            var formatter = DependencyResolver.Container.GetInstance<IFormatter<DataTable>>();
            var formatted = formatter.Format(_queried);

            var template = new XLTemplate(_templateFilePath);

            // TODO: Remove hardcoded values/refactor
            template.AddVariable("Users", formatted);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Microsoft Excel Spreadsheet (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                string outputFilePath = saveFileDialog.FileName;

                var renderer = new ExcelRenderer();
                renderer.Render(outputFilePath, template);

                MessageBox.Show("Report generated", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void QueryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            QueryGoButton.IsEnabled = QueryTextBox.Text.Length > 0;
        }

        private void ChangeControlsState(bool connectionStringTextBoxIsEnabled, bool connectionTestButtonIsEnabled, bool queryTextBoxActiveIsEnabled, bool queryGoButtonIsEnabled, bool loadTemplateButtonIsEnabled, bool exportToExcelButtonIsEnabled)
        {
            ConnectionStringTextBox.IsEnabled = connectionStringTextBoxIsEnabled;
            ConnectionTestButton.IsEnabled = connectionTestButtonIsEnabled;
            QueryTextBox.IsEnabled = queryTextBoxActiveIsEnabled;
            QueryGoButton.IsEnabled = queryGoButtonIsEnabled;
            LoadTemplateButton.IsEnabled = loadTemplateButtonIsEnabled;
            ExportToXlsButton.IsEnabled = exportToExcelButtonIsEnabled;
        }
    }
}
