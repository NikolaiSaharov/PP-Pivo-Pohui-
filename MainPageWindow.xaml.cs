using System.Windows;

namespace FirstTask
{
    public partial class MainPageWindow : Window
    {
        public MainPageWindow()
        {
            InitializeComponent();
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            Close();
            startWindow.Show();
        }
    }
}
