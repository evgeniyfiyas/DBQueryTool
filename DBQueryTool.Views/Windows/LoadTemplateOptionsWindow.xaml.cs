using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DBQueryTool.DataAccess.Models;
using DBQueryTool.DataAccess.Service.Interfaces;
using Microsoft.Win32;

namespace DBQueryTool.Views.Windows
{
    /// <summary>
    ///     Interaction logic for LoadTemplateOptionsWindow.xaml
    /// </summary>
    public partial class LoadTemplateOptionsWindow : WindowBase, IDisposable
    {
        private readonly ITemplateService _templateService;

        public LoadTemplateOptionsWindow(ITemplateService templateService)
        {
            InitializeComponent();
            _templateService = templateService;
        }

        public Template Result { get; private set; }

        public void Dispose()
        {
            // Dispose objects here...
        }

        private void ChooseLocalTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "xlsx files (*.xlsx)|*.xlsx"
            };
            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                Logger.Info("Template filepath provided successfully.");
                Result = new Template
                {
                    CreatedAt = new DateTime(),
                    Name = "Local File",
                    TemplateFileBytes = ReadFile(openFileDialog.FileName),
                    TypeId = -1 // TODO: Change hardcoded value
                };
                MessageBox.Show("Template loaded", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SaveToDbCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            TemplateNameTextBox.IsEnabled = SaveToDbCheckBox.IsChecked == true;
        }

        private void UploadLocalTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            if (SaveToDbCheckBox.IsChecked == true)
            {
                var templateName = TemplateNameTextBox.Text.Trim();
                if (Result == null)
                {
                    MessageBox.Show("Template is not loaded.", "DBQueryTool", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                if (templateName.Length == 0)
                {
                    MessageBox.Show("Template name can't be empty!", "DBQueryTool", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }


                if (_templateService.GetAll().Any(o => o.Name == templateName))
                {
                    MessageBox.Show("This template name already exists!", "DBQueryTool", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                _templateService.Add(new Template
                {
                    Name = templateName,
                    TypeId = 0, // TODO: Remove hardcoded values
                    TemplateFileBytes = Result.TemplateFileBytes
                });
            }

            DialogResult = Result != null;
            Close();
        }

        private byte[] ReadFile(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                return br.ReadBytes((int) new FileInfo(path).Length);
            }
        }

        private void WindowBase_Loaded(object sender, RoutedEventArgs e)
        {
            var templateViewSource = (CollectionViewSource) FindResource("templateViewSource");
            templateViewSource.Source = _templateService.GetObservableCollection();
        }

        private void TemplateDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (templateDataGrid.SelectedItem == null) return;
            Result = templateDataGrid.SelectedItem as Template;
            DialogResult = true;
            Close();
        }
    }
}