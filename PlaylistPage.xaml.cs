using System.Windows;
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
            InitializeEllipsisButton();
        }

        private void InitializeTrackSelection()
        {
            foreach (var track in TracksStackPanel.Children)
            {
                if (track is Border border)
                {
                    border.MouseEnter += Track_MouseEnter;
                    border.MouseLeave += Track_MouseLeave;
                    border.MouseLeftButtonDown += Track_MouseLeftButtonDown;
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

        private void Track_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#090909"));
            }
        }

        private void Track_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.Background = Brushes.Transparent;
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
                selectedTrack.Background = new SolidColorBrush(Colors.DarkGray);
            }
        }
    }
}
