using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClosedXML.Report;
using DBQueryTool.DataAccess.DataProviders;
using DBQueryTool.DataAccess.Models;
using Microsoft.Win32;

namespace DBQueryTool.Views.Windows
{
    /// <summary>
    /// Interaction logic for LoadTemplateOptionsWindow.xaml
    /// </summary>
    public partial class LoadTemplateOptionsWindow : WindowBase, IDisposable
    {
        public Template Result { get; private set; }

        public LoadTemplateOptionsWindow()
        {
            InitializeComponent();
        }

        private void ChooseLocalTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "xlsx files (*.xlsx)|*.xlsx"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Logger.Info("Template filepath provided successfully.");
                Result = new Template
                {
                    CreatedAt = new DateTime(),
                    Name = "Local File",
                    TemplateFileBytes = ReadFile(openFileDialog.FileName),
                    TypeId = -1
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
                    MessageBox.Show("Template is not loaded.", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (templateName.Length == 0)
                {
                    MessageBox.Show("Template name can't be empty!", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    using (var dbContext = new DBQueryToolDbContext())
                    {
                        if (dbContext.Templates.Any(o => o.Name == templateName))
                        {
                            MessageBox.Show("This template name already exists!", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                        {
                            dbContext.Templates.Add(new Template()
                            {
                                Name = templateName,
                                TypeId = 0,
                                TemplateFileBytes = Result.TemplateFileBytes,
                            });
                            dbContext.SaveChanges();
                        }
                    }
                }
            } 

            this.DialogResult = Result != null;
            this.Close();
        }

        public void Dispose()
        {
            // Dispose objects here...
        }

        private byte[] ReadFile(string path)
        {
            byte[] data = null;

            var fileInfo = new FileInfo(path);
            var numBytes = fileInfo.Length;

            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            var br = new BinaryReader(fileStream);

            data = br.ReadBytes((int)numBytes);
            br.Close();
            fileStream.Close();

            return data;
        }

        private void WindowBase_Loaded(object sender, RoutedEventArgs e)
        {
            var templateViewSource = ((CollectionViewSource)(this.FindResource("templateViewSource")));
            using (var context = new DBQueryToolDbContext())
            {
                context.Templates.Load();
                templateViewSource.Source = context.Templates.Local;
            }
        }

        private void TemplateDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (templateDataGrid.SelectedItem == null) return;
            Result = templateDataGrid.SelectedItem as Template;
            this.DialogResult = true;
            this.Close();
        }
    }
}
