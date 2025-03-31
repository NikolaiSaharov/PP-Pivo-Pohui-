using System.Windows;
using System.Windows.Controls;

namespace FirstTask
{
    public partial class ArtistPage : Page
    {
        public ArtistPage()
        {
            InitializeComponent();
        }

        private void primeBtn_Click(object sender, RoutedEventArgs e)
        {
            primeGrd.Visibility = Visibility.Visible;
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            primeGrd.Visibility = Visibility.Hidden;
        }

        private void unsubscribeBtn_Click(object sender, RoutedEventArgs e)
        {
            buttonPanels.Visibility = Visibility.Hidden;
            subscribeBtn.Visibility = Visibility.Visible;
        }

        private void subscribeBtn_Click(object sender, RoutedEventArgs e)
        {
            buttonPanels.Visibility = Visibility.Visible;
            subscribeBtn.Visibility = Visibility.Hidden;
        }
    }
}
