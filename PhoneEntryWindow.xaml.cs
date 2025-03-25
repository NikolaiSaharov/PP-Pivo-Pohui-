using System.Windows;

namespace FirstTask
{
    public partial class PhoneEntryWindow : Window
    {
        public PhoneEntryWindow()
        {
            InitializeComponent();
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void continueBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            Close();
            registrationWindow.Show();
        }
    }
}
