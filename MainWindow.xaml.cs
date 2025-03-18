using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls.Primitives;

namespace CHOTOPOHOZEENASPOTIK
{
    public partial class MainWindow : Window
    {
        private readonly DoubleAnimation slideAnimation;
        private readonly ThicknessAnimation marginAnimation;
        private readonly ColorAnimation[] gradientAnimations;
        private bool isRightPanelExpanded = false;
        private bool isPlaying = false;
        private bool isShuffleEnabled = false;
        private bool isRepeatEnabled = false;
        private bool isInfoPanelExpanded = false;
        private readonly DoubleAnimation infoPanelAnimation;
        private readonly ThicknessAnimation contentMarginAnimation;

        public MainWindow()
        {
            InitializeComponent();

            // Настройка анимации выдвижения панели
            slideAnimation = new DoubleAnimation
            {
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            // Настройка анимации сдвига контента
            marginAnimation = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            // Инициализация массива анимаций для градиента
            gradientAnimations = new ColorAnimation[9];
            for (int i = 0; i < gradientAnimations.Length; i++)
            {
                gradientAnimations[i] = new ColorAnimation
                {
                    Duration = TimeSpan.FromSeconds(5),
                    AutoReverse = true,
                    RepeatBehavior = RepeatBehavior.Forever
                };
            }

            // Инициализация анимации для информационной панели
            infoPanelAnimation = new DoubleAnimation
            {
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            // Инициализация анимации для отступа контента
            contentMarginAnimation = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            // Добавляем обработчики событий наведения мыши
            InfoPanel.MouseEnter += InfoPanel_MouseEnter;
            InfoPanel.MouseLeave += InfoPanel_MouseLeave;

            // Инициализация анимации градиента
            StartGradientAnimation();

            // Добавление примеров треков
            AddSampleTracks();

            InitializePlayerControls();
        }

        private void ToggleInfoPanel_Click(object sender, RoutedEventArgs e)
        {
            if (!isInfoPanelExpanded)
            {
                // Открываем панель
                infoPanelAnimation.From = 0;
                infoPanelAnimation.To = 240;

                // Анимация для контента
                contentMarginAnimation.From = mainContent.Margin;
                contentMarginAnimation.To = new Thickness(0, 0, 240, 0);

                // Поворачиваем иконку
                var icon = ((Button)sender).Content as PackIcon;
                if (icon != null)
                {
                    icon.Kind = PackIconKind.ChevronRight;
                }

                InfoPanel.BeginAnimation(FrameworkElement.WidthProperty, infoPanelAnimation);
                mainContent.BeginAnimation(FrameworkElement.MarginProperty, contentMarginAnimation);
            }
            else
            {
                // Закрываем панель
                infoPanelAnimation.From = 240;
                infoPanelAnimation.To = 0;

                // Анимация для контента
                contentMarginAnimation.From = mainContent.Margin;
                contentMarginAnimation.To = new Thickness(0);

                // Возвращаем иконку
                var icon = ((Button)sender).Content as PackIcon;
                if (icon != null)
                {
                    icon.Kind = PackIconKind.ChevronLeft;
                }

                InfoPanel.BeginAnimation(FrameworkElement.WidthProperty, infoPanelAnimation);
                mainContent.BeginAnimation(FrameworkElement.MarginProperty, contentMarginAnimation);
            }
            
            isInfoPanelExpanded = !isInfoPanelExpanded;
        }

        private void InfoPanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isInfoPanelExpanded)
            {
                var animation = new DoubleAnimation
                {
                    From = 0,
                    To = 20,
                    Duration = TimeSpan.FromSeconds(0.2),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                };

                InfoPanel.BeginAnimation(FrameworkElement.WidthProperty, animation);
            }
        }

        private void InfoPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isInfoPanelExpanded)
            {
                var animation = new DoubleAnimation
                {
                    From = 20,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.2),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                };

                InfoPanel.BeginAnimation(FrameworkElement.WidthProperty, animation);
            }
        }

        private void StartGradientAnimation()
        {
            var gradient = (LinearGradientBrush)BackgroundRect.Fill;

            // Настройка анимаций для каждого цвета
            ConfigureAnimation(gradientAnimations[0], Color.FromRgb(183, 0, 255), Color.FromRgb(0, 40, 255), gradient.GradientStops[0]);
            ConfigureAnimation(gradientAnimations[1], Color.FromRgb(0, 40, 255), Color.FromRgb(70, 207, 255), gradient.GradientStops[1]);
            ConfigureAnimation(gradientAnimations[2], Color.FromRgb(70, 207, 255), Color.FromRgb(81, 28, 232), gradient.GradientStops[2]);
            ConfigureAnimation(gradientAnimations[3], Color.FromRgb(81, 28, 232), Color.FromRgb(170, 145, 255), gradient.GradientStops[3]);
            ConfigureAnimation(gradientAnimations[4], Color.FromRgb(170, 145, 255), Color.FromRgb(78, 115, 255), gradient.GradientStops[4]);
            ConfigureAnimation(gradientAnimations[5], Color.FromRgb(78, 115, 255), Color.FromRgb(210, 0, 255), gradient.GradientStops[5]);
            ConfigureAnimation(gradientAnimations[6], Color.FromRgb(210, 0, 255), Color.FromRgb(0, 16, 200), gradient.GradientStops[6]);
            ConfigureAnimation(gradientAnimations[7], Color.FromRgb(0, 16, 200), Color.FromRgb(136, 0, 255), gradient.GradientStops[7]);
            ConfigureAnimation(gradientAnimations[8], Color.FromRgb(136, 0, 255), Color.FromRgb(183, 0, 255), gradient.GradientStops[8]);
        }

        private void ConfigureAnimation(ColorAnimation animation, Color from, Color to, GradientStop gradientStop)
        {
            animation.From = from;
            animation.To = to;
            animation.Duration = TimeSpan.FromSeconds(10);
            animation.AutoReverse = true;
            animation.RepeatBehavior = RepeatBehavior.Forever;
            gradientStop.BeginAnimation(GradientStop.ColorProperty, animation);
        }

        private void AddSampleTracks()
        {
            string[] trackNames = { "Трек 1", "Трек 2", "Трек 3", "Трек 4" };
            string[] artists = { "Исполнитель 1", "Исполнитель 2", "Исполнитель 3", "Исполнитель 4" };

            for (int i = 0; i < trackNames.Length; i++)
            {
                var card = new Card
                {
                    Width = 200,
                    Height = 250,
                    Margin = new Thickness(10),
                    Background = new SolidColorBrush(Color.FromRgb(47, 47, 47))
                };

                var grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(160) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                var image = new Image
                {
                    Source = new System.Windows.Media.Imaging.BitmapImage(
                        new Uri("pack://application:,,,/CHOTOPOHOZEENASPOTIK;component/Images/placeholder.jpg")),
                    Stretch = Stretch.UniformToFill
                };

                var stackPanel = new StackPanel { Margin = new Thickness(8) };

                var titleBlock = new TextBlock
                {
                    Text = trackNames[i],
                    Style = (Style)FindResource("MaterialDesignBody1TextBlock"),
                    Foreground = Brushes.White
                };

                var artistBlock = new TextBlock
                {
                    Text = artists[i],
                    Style = (Style)FindResource("MaterialDesignCaptionTextBlock"),
                    Foreground = Brushes.Gray
                };

                stackPanel.Children.Add(titleBlock);
                stackPanel.Children.Add(artistBlock);

                Grid.SetRow(image, 0);
                Grid.SetRow(stackPanel, 1);

                grid.Children.Add(image);
                grid.Children.Add(stackPanel);

                card.Content = grid;

                TracksContainer.Children.Add(card);
            }
        }

        private void InitializePlayerControls()
        {
            // Обработчик для кнопки Play/Pause
            playButton.Click += (s, e) =>
            {
                isPlaying = !isPlaying;
                var icon = (PackIcon)playButton.Content;
                icon.Kind = isPlaying ?
                    PackIconKind.Pause :
                    PackIconKind.Play;
            };

            // Обработчик для кнопки Shuffle
            shuffleButton.Click += (s, e) =>
            {
                isShuffleEnabled = !isShuffleEnabled;
                shuffleButton.Foreground = isShuffleEnabled ?
                    Brushes.White :
                    new SolidColorBrush(Color.FromRgb(128, 128, 128));
            };

            // Обработчик для кнопки Repeat
            repeatButton.Click += (s, e) =>
            {
                isRepeatEnabled = !isRepeatEnabled;
                repeatButton.Foreground = isRepeatEnabled ?
                    Brushes.White :
                    new SolidColorBrush(Color.FromRgb(128, 128, 128));
            };

            // Обработчик для кнопки добавления в плейлист
            addToPlaylistButton.Click += (s, e) =>
            {
                MessageBox.Show("Трек добавлен в любимые");
            };
        }

        private void ShowSection(string section)
        {
            // Очищаем текущий контент
            mainContent.Children.Clear();

            // Создаем серый фон для раздела
            var background = new Rectangle
            {
                Fill = new SolidColorBrush(Color.FromRgb(47, 47, 47)),
                Stretch = Stretch.Fill
            };

            mainContent.Children.Add(background);

            // Добавляем заголовок раздела
            var title = new TextBlock
            {
                Text = section,
                Foreground = Brushes.White,
                FontSize = 24,
                Margin = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            mainContent.Children.Add(title);
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            // Сброс цвета всех кнопок
            foreach (var child in ((UniformGrid)button.Parent).Children)
            {
                if (child is Button btn)
                {
                    btn.Foreground = Brushes.Gray;
                }
            }

            // Установка цвета выбранной кнопки
            button.Foreground = Brushes.White;

            // Фильтрация контента
            string filter = button.Content.ToString();
            FilterTracks(filter);
        }

        private void FilterTracks(string filter)
        {
            // Пример фильтрации
            foreach (var card in TracksContainer.Children)
            {
                if (card is Card trackCard)
                {
                    trackCard.Visibility = filter == "Все" ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void PlanetButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSection("Главная");
        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSection("Библиотека");
        }

        private void PersonButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSection("Профиль");
        }

        private void PlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSection("Плейлист");
        }
    }
}
