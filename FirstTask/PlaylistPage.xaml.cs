using CHOTOPOHOZEENASPOTIK;
using Microsoft.Xaml.Behaviors;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FirstTask
{
    public partial class PlaylistPage : Page
    {
        private bool isPlaying = false;
        private TrackControl currentlyPlayingTrack;
        public PlaylistPage()
        {
            InitializeComponent();
            InitializeTrackSelection();
            InitializeEllipsisButton();
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

        private void InitializeEllipsisButton()
        {
            // Находим StackPanel, содержащую Border с изображением
            var mainGrid = (Grid)this.Content;
            var leftStackPanel = (StackPanel)mainGrid.Children[0];
            var imageBorder = (Border)leftStackPanel.Children[0];

            imageBorder.MouseEnter += (s, e) => EllipsisButton.Opacity = 1;
            imageBorder.MouseLeave += (s, e) => EllipsisButton.Opacity = 0;
        }

        private void TrackControl_OnTrackClicked(object sender, TrackControl clickedTrack)
        {
            if (currentlyPlayingTrack == clickedTrack)
            {
                TogglePlayPause();
                return;
            }

            ResetPreviousTrack();

            currentlyPlayingTrack = clickedTrack;
            UpdateBehaviour();
        }

        private void UpdateBehaviour(bool isPlaying = true)
        {
            if (currentlyPlayingTrack != null)
            {
                currentlyPlayingTrack.Background = isPlaying ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#090909")) : Brushes.Transparent;
                currentlyPlayingTrack.AnimationPlaceholder.Content = isPlaying
                    ? new EqualizerControl()
                    : null;
            }

            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow?.playButton != null)
            {
                var behaviors = Interaction.GetBehaviors(mainWindow.playButton);
                var playBehavior = behaviors.OfType<PlayPauseButtonBehavior>().FirstOrDefault();
                playBehavior.IsPlaying = isPlaying;
            }
        }

        private void TogglePlayPause()
        {
            isPlaying = !isPlaying;

            UpdateBehaviour(isPlaying);
        }

        private void ResetPreviousTrack()
        {
            if (currentlyPlayingTrack != null)
            {
                currentlyPlayingTrack.Background = Brushes.Transparent;
                currentlyPlayingTrack.AnimationPlaceholder.Content = null;
            }
        }

    }
}
