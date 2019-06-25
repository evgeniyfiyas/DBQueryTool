using System.Windows;
using DBQueryTool.UserService;

namespace DBQueryTool.Views.Windows
{
    /// <summary>
    ///     Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : WindowBase
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var authResult = new Auth().Authenticate();

            if (authResult)
            {
                var mainWindow = DependencyResolver.DependencyResolver.Container.GetInstance<MainWindow>();
                mainWindow.Show();
                Close();
            }
        }
    }
}