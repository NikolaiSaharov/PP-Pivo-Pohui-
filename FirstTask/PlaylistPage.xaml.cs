using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FirstTask
{
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
                if (track is TrackControl trackControl)
                {
                    trackControl.OnTrackClicked += TrackControl_OnTrackClicked;
                }
            }
        }

        private void TrackControl_OnTrackClicked(object sender, TrackControl clickedTrack)
        {
            // Сброс подсветки и анимации для всех треков
            foreach (var track in TracksStackPanel.Children)
            {
                if (track is TrackControl trackControl)
                {
                    trackControl.Background = Brushes.Transparent; // Сброс подсветки
                    trackControl.AnimationPlaceholder.Content = null; // Удаление анимации
                }
            }

            // Подсветка и анимация для выбранного трека
            clickedTrack.Background = new SolidColorBrush(Colors.LightGray); // Подсветка
            clickedTrack.AnimationPlaceholder.Content = new EqualizerControl(); // Анимация
        }
    }
}
