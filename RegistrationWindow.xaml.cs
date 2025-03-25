using System.Windows;

namespace FirstTask
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void continueBtn_Click(object sender, RoutedEventArgs e)
        {
            CommunityWindow communityWindow = new CommunityWindow();
            Close();
            communityWindow.Show();
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
