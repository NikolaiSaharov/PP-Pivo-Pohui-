using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Sound_Player
{
    public partial class FullPlayerPage : Page
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private bool isPlaying = false;
        private bool isRepeat = false;
        private readonly SolidColorBrush defaultBackground = new SolidColorBrush(Color.FromRgb(0x12, 0x12, 0x12));

        // Для управления активными кнопками
        private Button activeButton;
        private readonly SolidColorBrush activeBrush = new SolidColorBrush(Colors.White);
        private readonly SolidColorBrush inactiveBrush = new SolidColorBrush(Color.FromRgb(0x80, 0x80, 0x80));

        public event EventHandler CloseRequested;

        public FullPlayerPage()
        {
            InitializeComponent();
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;

            // Устанавливаем первую кнопку как активную по умолчанию
            SetActiveButton(PreviousButton);
        }

        private void SetActiveButton(Button button)
        {
            // Если уже есть активная кнопка - делаем её неактивной
            if (activeButton != null)
            {
                activeButton.Foreground = inactiveBrush;
            }

            // Устанавливаем новую активную кнопку
            activeButton = button;
            activeButton.Foreground = activeBrush;
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            SetActiveButton(PreviousButton);
            try
            {
                CoverImageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/FullPleer.jpg"));
                MainBorder.Background = defaultBackground;
                AnimateTrackCoverSize(400, 400);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в PreviousButton: {ex.Message}");
            }
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            SetActiveButton(PlayPauseButton);
            try
            {
                if (isPlaying)
                {
                    mediaPlayer.Pause();
                    isPlaying = false;
                }
                else
                {
                    mediaPlayer.Play();
                    isPlaying = true;
                    UpdateBackgroundFromImage();
                }

                CoverImageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/FullPleer.jpg"));
                AnimateTrackCoverSize(400, 400);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в PlayPauseButton: {ex.Message}");
            }
        }

        private void UpdateBackgroundFromImage()
        {
            var imageBrush = CoverImageBrush;
            if (imageBrush?.ImageSource is BitmapSource bitmapSource)
            {
                try
                {
                    var colors = ColorExtractor.GetDominantColors(bitmapSource, 2);
                    var gradientBrush = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(1, 1),
                        GradientStops = new GradientStopCollection
                        {
                            new GradientStop(colors[0], 0),
                            new GradientStop(colors[1], 1)
                        }
                    };
                    MainBorder.Background = gradientBrush;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении фона: {ex.Message}");
                    MainBorder.Background = defaultBackground;
                }
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            SetActiveButton(NextButton);
            try
            {
                CoverImageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/FullPleer.jpg"));
                UpdateBackgroundFromImage();
                AnimateTrackCoverSize(450, 850);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в NextButton: {ex.Message}");
                CoverImageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/FullPleer.jpg"));
                AnimateTrackCoverSize(400, 400);
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mediaPlayer.Stop();
                isPlaying = false;

                CoverImageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/FullPleer.jpg"));
                MainBorder.Background = defaultBackground;
                AnimateTrackCoverSize(400, 400);
                CloseRequested?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в StopButton: {ex.Message}");
            }
        }

        private void AnimateTrackCoverSize(double targetWidth, double targetHeight)
        {
            targetWidth = Math.Min(targetWidth, MainBorder.ActualWidth * 0.9);
            targetHeight = Math.Min(targetHeight, MainBorder.ActualHeight * 0.9);

            var widthAnimation = new DoubleAnimation
            {
                To = targetWidth,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            var heightAnimation = new DoubleAnimation
            {
                To = targetHeight,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            TrackCoverBorder.CornerRadius = new CornerRadius(Math.Min(targetWidth, targetHeight) * 0.1);
            TrackCoverBorder.BeginAnimation(Border.WidthProperty, widthAnimation);
            TrackCoverBorder.BeginAnimation(Border.HeightProperty, heightAnimation);
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.ContextMenu != null)
            {
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
            }
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            if (isRepeat)
            {
                mediaPlayer.Position = TimeSpan.Zero;
                mediaPlayer.Play();
            }
            else
            {
                isPlaying = false;
            }
        }

        private void OnMeretMenuItemClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Otvet());
        }

        private void CreateMix_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Otvet());
        }

        private void Remix_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Otvet());
        }

        private void AddToLibrary_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Otvet());
        }

        private void AddToQueue_Click(object sender, RoutedEventArgs e)
        {
            // Логика для "Добавить в очередь"
        }

        private void MixByArtist_Click(object sender, RoutedEventArgs e)
        {
            // Логика для "Микс по исполнителю"
        }

        private void AddToPlaylist_Click(object sender, RoutedEventArgs e)
        {
            // Логика для "Добавить в плейлист"
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            // Логика для "Поделиться"
        }
    }
}