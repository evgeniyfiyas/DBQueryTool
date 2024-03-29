﻿using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using ClosedXML.Report;
using DBQueryTool.ConnectionProcessor.DataProviders;
using DBQueryTool.DataAccess.Models;
using DBQueryTool.ReportProcessor.Formatters;
using DBQueryTool.ReportProcessor.Renderers;

namespace DBQueryTool.Views.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        private readonly IFormatter<DataTable> _formatter;
        private readonly IRenderer _renderer;
        private List<string> _availableProvidersNames;

        private IDataProvider _dataProvider;
        private DataTable _queried;
        private Template _template;

        public MainWindow(IFormatter<DataTable> formatter, IRenderer renderer)
        {
            InitializeComponent();
            _formatter = formatter;
            _renderer = renderer;
        }

        private void ConnectionTestButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProviderName = DatabaseProvidersComboBox.Text;
            _dataProvider = DependencyResolver.DependencyResolver.Container.GetAllInstances<IDataProvider>()
                .First(o => o.VisibleName == selectedProviderName);

            _dataProvider.Build(ConnectionStringTextBox.Text);
            var result = _dataProvider.TestConnection();
            if (result == true)
            {
                MessageBox.Show("Connected.", "DBQueryTool: Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ChangeControlsState(false, false, true, false, true, false);
            }
            else if (result == false)
            {
                MessageBox.Show("Invalid connection string.", "DBQueryTool: Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Connection string is not specified.", "DBQueryTool: Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
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
            using (var loadTemplateOptionsWindow =
                DependencyResolver.DependencyResolver.Container.GetInstance<LoadTemplateOptionsWindow>())
            {
                var dialogResult = loadTemplateOptionsWindow.ShowDialog();
                if (dialogResult == true)
                {
                    _template = loadTemplateOptionsWindow.Result;
                    ExportToXlsButton.IsEnabled = true;
                }
            }
        }

        private void ExportToXlsButton_Click(object sender, RoutedEventArgs e)
        {
            var formatted = _formatter.Format(_queried);

            var xltemplate = new XLTemplate(new MemoryStream(_template.TemplateFileBytes));


            // TODO: Remove hardcoded values/refactor
            xltemplate.AddVariable("Users", formatted);

            if (_renderer.Render(xltemplate))
                MessageBox.Show("Report generated", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("There was a problem generating your report", "DBQueryTool", MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }

        private void QueryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var regex = new Regex(@"(?i)(SELECT).*");
            var match = regex.Match(QueryTextBox.Text);
            QueryGoButton.IsEnabled = QueryTextBox.Text.Length > 0 && match.Success;
        }

        private void ChangeControlsState(bool connectionStringTextBoxIsEnabled, bool connectionTestButtonIsEnabled,
            bool queryTextBoxActiveIsEnabled, bool queryGoButtonIsEnabled, bool loadTemplateButtonIsEnabled,
            bool exportToExcelButtonIsEnabled)
        {
            ConnectionStringTextBox.IsEnabled = connectionStringTextBoxIsEnabled;
            ConnectionTestButton.IsEnabled = connectionTestButtonIsEnabled;
            QueryTextBox.IsEnabled = queryTextBoxActiveIsEnabled;
            QueryGoButton.IsEnabled = queryGoButtonIsEnabled;
            LoadTemplateButton.IsEnabled = loadTemplateButtonIsEnabled;
            ExportToXlsButton.IsEnabled = exportToExcelButtonIsEnabled;
        }

        private void DatabaseProvidersComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var providerName in _availableProvidersNames) DatabaseProvidersComboBox.Items.Add(providerName);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _availableProvidersNames = DependencyResolver.DependencyResolver.Container.GetAllInstances<IDataProvider>()
                .Select(o => o.VisibleName).ToList();
        }
    }
}