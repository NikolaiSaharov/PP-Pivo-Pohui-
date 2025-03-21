using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FirstTask
{
    /// <summary>
    /// Логика взаимодействия для PlaylistPage.xaml
    /// </summary>
    public partial class PlaylistPage : Page
    {
        public PlaylistPage()
        {
            InitializeComponent();
            InitializeTrackSelection();
        }
        private void InitializeTrackSelection()
        {
            foreach (var track in TracksStackPanel.Children)
            {
                if (track is Border border)
                {
                    border.MouseLeftButtonDown += Track_MouseLeftButtonDown;
                }
            }
        }
        private void Track_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border selectedTrack)
            {
                // Сброс подсветки всех треков
                foreach (var track in TracksStackPanel.Children)
                {
                    if (track is Border border)
                    {
                        border.Background = Brushes.Transparent;
                    }
                }

                // Подсветка выбранного трека
                selectedTrack.Background = new SolidColorBrush(Colors.LightGray);
            }
        }
    }
}
