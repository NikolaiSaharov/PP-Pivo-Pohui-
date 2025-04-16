using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Sound_Player
{
    public partial class FullPlayerPage : Page
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private bool isPlaying = false;
        private bool isRepeat = false;
        private readonly SolidColorBrush defaultBackground = new SolidColorBrush(Color.FromRgb(0x12, 0x12, 0x12));

        private Button activeButton;
        private readonly SolidColorBrush activeBrush = new SolidColorBrush(Colors.White);
        private readonly SolidColorBrush inactiveBrush = new SolidColorBrush(Color.FromRgb(0x80, 0x80, 0x80));

        public event EventHandler CloseRequested;

        public FullPlayerPage()
        {
            InitializeComponent();
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            SetActiveButton(PreviousButton);
            BackgroundVideo.Volume = 0; // Отключаем звук видео
            this.Loaded += FullPlayerPage_Loaded;
        }

        private void FullPlayerPage_Loaded(object sender, RoutedEventArgs e)
        {
            PlayVideo(); // Автоматически запускаем видео при загрузке
        }

        private void PlayVideo()
        {
            try
            {
                string videoFileName = @"jujutsu.mp4";
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, videoFileName);

                if (File.Exists(fullPath))
                {
                    BackgroundVideo.Source = new Uri(fullPath);
                    ShowVideo();
                }
                else
                {
                    MessageBox.Show("Видео не найдено по пути:\n" + fullPath);
                    MainBorder.Background = defaultBackground;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при воспроизведении видео: {ex.Message}");
            }
        }

        private void SetActiveButton(Button button)
        {
            if (activeButton != null)
            {
                activeButton.Foreground = inactiveBrush;
            }
            activeButton = button;
            activeButton.Foreground = activeBrush;
        }

        private void ShowVideo()
        {
            BackgroundVideo.Visibility = Visibility.Visible;
            BackgroundVideo.Play();
        }

        private void HideVideo()
        {
            BackgroundVideo.Stop();
            BackgroundVideo.Visibility = Visibility.Collapsed;
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            SetActiveButton(PreviousButton);
            try
            {
                // Останавливаем видео трека и показываем изображение
                TrackVideoPlayer.Stop();
                TrackVideoPlayer.Visibility = Visibility.Collapsed;
                CoverImageBrush.Opacity = 1;

                // Запускаем фоновое видео
                PlayVideo();
                CoverImageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/FullPleer.jpg"));
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
                    TrackVideoPlayer.Pause();
                    isPlaying = false;
                }
                else
                {
                    mediaPlayer.Play();
                    if (TrackVideoPlayer.Visibility == Visibility.Visible)
                    {
                        TrackVideoPlayer.Play();
                    }
                    isPlaying = true;
                    UpdateBackgroundFromImage();
                }

                HideVideo();
                TrackVideoPlayer.Stop();
                TrackVideoPlayer.Visibility = Visibility.Collapsed;
                CoverImageBrush.Opacity = 1;
                UpdateBackgroundFromImage();
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
                    MainBorder.Background = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(1, 1),
                        GradientStops = new GradientStopCollection
                        {
                            new GradientStop(colors[0], 0),
                            new GradientStop(colors[1], 1)
                        }
                    };
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
                // Скрываем фоновое видео
                HideVideo();

                // Загружаем и воспроизводим видео в TrackCoverBorder
                PlayTrackVideo();

                // Анимируем размер TrackCoverBorder
                AnimateTrackCoverSize(450, 850);

                // Обновляем фон на основе видео
                UpdateBackgroundFromVideo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в NextButton: {ex.Message}");
                ShowDefaultTrackImage();
                AnimateTrackCoverSize(400, 400);
            }
        }
        private void UpdateBackgroundFromVideo()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    // Даем небольшой таймаут, чтобы видео точно было готово
                    var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
                    timer.Tick += (s, args) =>
                    {
                        timer.Stop();
                        var bitmapSource = GetVideoFrame(TrackVideoPlayer);
                        if (bitmapSource != null)
                        {
                            var colors = ColorExtractor.GetDominantColors(bitmapSource, 2);
                            MainBorder.Background = new LinearGradientBrush
                            {
                                StartPoint = new Point(0, 0),
                                EndPoint = new Point(1, 1),
                                GradientStops = new GradientStopCollection
                        {
                            new GradientStop(colors[0], 0),
                            new GradientStop(colors[1], 1)
                        }
                            };
                        }
                    };
                    timer.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении фона из видео: {ex.Message}");
                    MainBorder.Background = defaultBackground;
                }
            }));
        }

        private BitmapSource GetVideoFrame(MediaElement mediaElement)
        {
            if (mediaElement.Source == null || !mediaElement.HasVideo)
                return null;

            // Создаем RenderTargetBitmap с размерами видео
            var renderTargetBitmap = new RenderTargetBitmap(
                (int)mediaElement.ActualWidth,
                (int)mediaElement.ActualHeight,
                96, 96, PixelFormats.Pbgra32);

            // Рендерим текущий кадр
            renderTargetBitmap.Render(mediaElement);

            return renderTargetBitmap;
        }
        private void PlayTrackVideo()
        {
            try
            {
                string videoFileName = @"jujutsu.mp4";
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, videoFileName);

                if (File.Exists(fullPath))
                {
                    TrackVideoPlayer.Volume = 0;
                    // Подписываемся на событие открытия медиа
                    TrackVideoPlayer.MediaOpened += TrackVideoPlayer_MediaOpened;

                    // Скрываем изображение и показываем видео
                    CoverImageBrush.Opacity = 0;
                    TrackVideoPlayer.Source = new Uri(fullPath);
                    TrackVideoPlayer.Visibility = Visibility.Visible;
                    TrackVideoPlayer.Play();
                }
                else
                {
                    MessageBox.Show("Видео трека не найдено по пути:\n" + fullPath);
                    ShowDefaultTrackImage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при воспроизведении видео трека: {ex.Message}");
                ShowDefaultTrackImage();
            }
        }
        private void TrackVideoPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            // Отписываемся от события, чтобы не вызывалось многократно
            TrackVideoPlayer.MediaOpened -= TrackVideoPlayer_MediaOpened;

            // Обновляем фон на основе первого кадра видео
            UpdateBackgroundFromVideo();
        }
        private void TrackVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            TrackVideoPlayer.Position = TimeSpan.Zero;
            TrackVideoPlayer.Play();
        }

        private void ShowDefaultTrackImage()
        {
            TrackVideoPlayer.Visibility = Visibility.Collapsed;
            CoverImageBrush.Opacity = 1;
            CoverImageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/FullPleer.jpg"));
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mediaPlayer.Stop();
                isPlaying = false;
                HideVideo();
                MainBorder.Background = defaultBackground;
                CoverImageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/FullPleer.jpg"));
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

        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Position = TimeSpan.Zero;
            BackgroundVideo.Play();
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

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.ContextMenu != null)
            {
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
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
