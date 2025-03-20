using System.Windows;

namespace FirstTask
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            Close();
            profileWindow.Show();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            Close();
            loginWindow.Show();
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
