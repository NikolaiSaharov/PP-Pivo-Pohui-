using System.Windows;

namespace Sound_Player
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
