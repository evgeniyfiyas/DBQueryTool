﻿using System.Collections;
using ClosedXML.Report;
using DBQueryTool.Core;
using DBQueryTool.Core.Formatters;
using DBQueryTool.Models.DataProviders;
using DBQueryTool.Views.Renderers;
using Microsoft.Win32;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using DBQueryTool.Views;
using DBQueryTool.Views.Renderers.Wrappers;
using DocumentFormat.OpenXml.Wordprocessing;
using StructureMap.Pipeline;

namespace DBQueryTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        private DataTable _queried;
        private string _templateFilePath;
        private readonly IDataProvider _dataProvider;

        public MainWindow(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            InitializeComponent();
        }

        private void ConnectionTestButton_Click(object sender, RoutedEventArgs e)
        {
            _dataProvider.Build(ConnectionStringTextBox.Text);
            var result = _dataProvider.TestConnection();
            if (result == true)
            {
                MessageBox.Show("Connected.", "DBQueryTool: Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ChangeControlsState(false, false, true, false, true, false);
            }
            else if (result == false)
            {
                MessageBox.Show("Invalid connection string.", "DBQueryTool: Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Connection string is not specified.", "DBQueryTool: Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ConnectionStringTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConnectionTestButton.IsEnabled = ConnectionStringTextBox.Text.Length > 0;
        }

        private void QueryGoButton_Click(object sender, RoutedEventArgs e)
        {
            _queried = _dataProvider.Query(QueryTextBox.Text);

            Logger.Info("Database query success");
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
                Logger.Info("XLSX template loaded successfully.");
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

            var renderer = DependencyResolver.Container.GetInstance<IRenderer<ExcelRendererWrapper>>();
            var renderable = DependencyResolver.Container.GetInstance<ExcelRendererWrapper>(new ExplicitArguments().Set(template));

            if (renderer.Render(renderable))
            {
                MessageBox.Show("Report generated", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("There was a problem generating your report", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void QueryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var regex = new Regex(@"(?i)(SELECT).*");
            var match = regex.Match(QueryTextBox.Text);
            QueryGoButton.IsEnabled = QueryTextBox.Text.Length > 0 && match.Success;
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
