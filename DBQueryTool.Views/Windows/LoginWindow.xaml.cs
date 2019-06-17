using System.Windows;
using DBQueryTool.Core;

namespace DBQueryTool.Views.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : WindowBase
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var authResult = new Auth.Auth().Authenticate();

            if (authResult)
            {
                var mainWindow = DependencyResolver.Container.GetInstance<MainWindow>();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
