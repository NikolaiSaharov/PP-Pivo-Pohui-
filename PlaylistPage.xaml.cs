using CHOTOPOHOZEENASPOTIK;
using Microsoft.Xaml.Behaviors;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace FirstTask
{
    public partial class PlaylistPage : Page
    {
        private bool isPlaying = false;
        private TrackControl currentlyPlayingTrack;
        private Color[] albumColors = new Color[3];
        private LinearGradientBrush gradientBrush;
        private const int GradientStopsCount = 7; // Оптимальное количество для плавности

        public PlaylistPage()
        {
            InitializeComponent();
            InitializeTrackSelection();
            InitializeEllipsisButton();
            ExtractColorsFromAlbumImage();
            SetupUltraSmoothGradient();
        }

        private void ExtractColorsFromAlbumImage()
        {
            try
            {
                var mainGrid = (Grid)this.Content;
                var leftStackPanel = (StackPanel)mainGrid.Children[0];
                var imageBorder = (Border)leftStackPanel.Children[0];
                var gridInsideBorder = (Grid)imageBorder.Child;
                var image = (Image)gridInsideBorder.Children[0];

                if (image.Source is BitmapSource bitmapSource)
                {
                    albumColors = ColorExtractor.GetDominantColors(bitmapSource, 3);
                }
                else
                {
                    SetDefaultColors();
                }
            }
            catch
            {
                SetDefaultColors();
            }
        }

        private void SetDefaultColors()
        {
            albumColors = new Color[]
            {
                Color.FromRgb(0x91, 0x3A, 0xC7), // Фиолетовый
                Color.FromRgb(0x09, 0x09, 0x09), // Черный
                Color.FromRgb(0x9B, 0xC2, 0xDB)  // Голубой
            };
        }

        private void SetupUltraSmoothGradient()
        {
            var grid = (Grid)this.Content;

            // Создаем градиент с увеличенным количеством стопов
            gradientBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
                SpreadMethod = GradientSpreadMethod.Pad,
                ColorInterpolationMode = ColorInterpolationMode.SRgbLinearInterpolation
            };

            // Добавляем градиентные стопы с плавными переходами
            for (int i = 0; i < GradientStopsCount; i++)
            {
                double position = (double)i / (GradientStopsCount - 1);
                var color = GetInterpolatedColor(position);
                gradientBrush.GradientStops.Add(new GradientStop(color, position));
            }

            grid.Background = gradientBrush;

            // Настраиваем анимации
            SetupColorAnimations();
            SetupDirectionAnimations();
        }

        private Color GetInterpolatedColor(double position)
        {
            // Нормализуем позицию для базовых цветов
            double basePos = position * (albumColors.Length - 1);
            int index = (int)basePos;
            double ratio = basePos - index;

            if (index >= albumColors.Length - 1)
                return albumColors[albumColors.Length - 1]; // Исправлено для .NET 7

            return Color.FromRgb(
                (byte)(albumColors[index].R + (albumColors[index + 1].R - albumColors[index].R) * ratio),
                (byte)(albumColors[index].G + (albumColors[index + 1].G - albumColors[index].G) * ratio),
                (byte)(albumColors[index].B + (albumColors[index + 1].B - albumColors[index].B) * ratio));
        }

        private void SetupColorAnimations()
        {
            Random rand = new Random();

            for (int i = 0; i < GradientStopsCount; i++)
            {
                double position = (double)i / (GradientStopsCount - 1);
                var initialColor = GetInterpolatedColor(position);

                // Создаем вариацию цвета для анимации с ручным ограничением значений
                int r = initialColor.R + (rand.Next(30) - 15);
                int g = initialColor.G + (rand.Next(30) - 15);
                int b = initialColor.B + (rand.Next(30) - 15);

                // Ограничиваем значения вручную
                r = r < 0 ? 0 : r > 255 ? 255 : r;
                g = g < 0 ? 0 : g > 255 ? 255 : g;
                b = b < 0 ? 0 : b > 255 ? 255 : b;

                Color targetColor = Color.FromRgb((byte)r, (byte)g, (byte)b);

                var animation = new ColorAnimation
                {
                    From = initialColor,
                    To = targetColor,
                    Duration = TimeSpan.FromSeconds(10 + rand.Next(6)),
                    AutoReverse = true,
                    RepeatBehavior = RepeatBehavior.Forever,
                    BeginTime = TimeSpan.FromSeconds(i * 0.7),
                    EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseInOut }
                };

                gradientBrush.GradientStops[i].BeginAnimation(
                    GradientStop.ColorProperty, animation);
            }
        }

        private void SetupDirectionAnimations()
        {
            // Плавная анимация направления градиента
            var startAnim = new PointAnimation
            {
                From = new Point(0, 0),
                To = new Point(0.15, 0.15),
                Duration = TimeSpan.FromSeconds(22),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseInOut }
            };

            var endAnim = new PointAnimation
            {
                From = new Point(1, 1),
                To = new Point(0.85, 0.85),
                Duration = TimeSpan.FromSeconds(25),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                BeginTime = TimeSpan.FromSeconds(4),
                EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseInOut }
            };

            gradientBrush.BeginAnimation(LinearGradientBrush.StartPointProperty, startAnim);
            gradientBrush.BeginAnimation(LinearGradientBrush.EndPointProperty, endAnim);
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