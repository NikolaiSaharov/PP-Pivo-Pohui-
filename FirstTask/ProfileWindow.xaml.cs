using System.Windows;

namespace FirstTask
{
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            InitializeComponent();
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void continueBtn_Click(object sender, RoutedEventArgs e)
        {
            PhoneEntryWindow phoneEntryWindow = new PhoneEntryWindow();
            Close();
            phoneEntryWindow.Show();
        }
    }
}
