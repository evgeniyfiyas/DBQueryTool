using System;
using System.Collections.Generic;
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
        private string _templatePath;
        public object Result { get; private set; }

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
                _templatePath = openFileDialog.FileName;
                Result = new XLTemplate(_templatePath);
                MessageBox.Show("Template loaded", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SaveToDbCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            TemplateNameTextBox.IsEnabled = SaveToDbCheckBox.IsChecked == true;
        }

        private void UploadLocalTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            /*
             
            if (SaveToDbCheckBox.IsChecked == true)
            {
                var templateName = TemplateNameTextBox.Text.Trim();
                if (templateName.Length == 0)
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
                            var fs = new FileStream(_templatePath, FileMode.Open, FileAccess.Read);
                            dbContext.Templates.Add(new Template()
                            {
                                CreatedAt = new DateTime(),
                                Name = templateName,
                                TypeId = 0,
                                TemplateFile = fs,
                            });
                        }
                    }
                }
            } 

            */

            this.DialogResult = Result != null;
            this.Close();
        }

        public void Dispose()
        {
            // Dispose objects here...
        }
    }
}
