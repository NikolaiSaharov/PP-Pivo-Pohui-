using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FirstTask;
using MaterialDesignThemes.Wpf;

namespace CHOTOPOHOZEENASPOTIK
{
    public partial class Planet : Page
    {
        private readonly ColorAnimation[] gradientAnimations;
        private int currentLevel = 0;
        private readonly Dictionary<MenuItem, ColorAnimation[]> menuItemAnimations = new Dictionary<MenuItem, ColorAnimation[]>();

        private readonly Dictionary<string, Color[]> moodColors = new Dictionary<string, Color[]>
        {
            { "Ужасное", new Color[]
                {
                    Color.FromRgb(0xAB, 0x26, 0xE8),
                    Color.FromRgb(0xA1, 0x00, 0xFF),
                    Color.FromRgb(0x73, 0x00, 0xFF),
                    Color.FromRgb(0xDC, 0x9F, 0xFF),
                    Color.FromRgb(0xD7, 0x91, 0xFF),
                    Color.FromRgb(0xA1, 0x00, 0xFF),
                    Color.FromRgb(0xCB, 0x46, 0xFF),
                    Color.FromRgb(0xA1, 0x00, 0xFF),
                    Color.FromRgb(0xEB, 0x86, 0xFF)
                }
            },
            { "Плохое", new Color[]
                {
                    Color.FromRgb(0x43, 0x26, 0xE8),
                    Color.FromRgb(0x26, 0x00, 0xFF),
                    Color.FromRgb(0x00, 0x0D, 0xFF),
                    Color.FromRgb(0xA9, 0x9F, 0xFF),
                    Color.FromRgb(0x98, 0x91, 0xFF),
                    Color.FromRgb(0x62, 0x46, 0xFF),
                    Color.FromRgb(0x60, 0x1C, 0xE8),
                    Color.FromRgb(0x86, 0x88, 0xFF),
                    Color.FromRgb(0x40, 0x00, 0xFF)
                }
            },
            { "Тревожное", new Color[]
                {
                    Color.FromRgb(0x26, 0x53, 0xE8),
                    Color.FromRgb(0x00, 0x33, 0xFF),
                    Color.FromRgb(0x00, 0x48, 0xFF),
                    Color.FromRgb(0x9F, 0xB2, 0xFF),
                    Color.FromRgb(0x91, 0xC1, 0xFF),
                    Color.FromRgb(0x1C, 0x74, 0xE8),
                    Color.FromRgb(0x00, 0x6F, 0xFF),
                    Color.FromRgb(0x00, 0x33, 0xFF),
                    Color.FromRgb(0x46, 0x90, 0xFF)
                }
            },
            { "Прекрасное", new Color[]
                {
                    Color.FromRgb(0xE8, 0x8D, 0x26),
                    Color.FromRgb(0xFF, 0x88, 0x00),
                    Color.FromRgb(0xFF, 0x88, 0x00),
                    Color.FromRgb(0xFF, 0xCF, 0x9F),
                    Color.FromRgb(0xFF, 0xD1, 0x91),
                    Color.FromRgb(0xFF, 0x88, 0x00),
                    Color.FromRgb(0xFF, 0xB8, 0x46),
                    Color.FromRgb(0xFF, 0x80, 0x00),
                    Color.FromRgb(0xFF, 0xC6, 0x86)
                }
            },
            { "Хорошее", new Color[]
                {
                    Color.FromRgb(0xE8, 0xB4, 0x26),
                    Color.FromRgb(0xFF, 0xF6, 0x00),
                    Color.FromRgb(0xFF, 0xEA, 0x00),
                    Color.FromRgb(0xFF, 0xFF, 0x9F),
                    Color.FromRgb(0xFB, 0xFF, 0x91),
                    Color.FromRgb(0xE8, 0xD7, 0x1C),
                    Color.FromRgb(0xFF, 0xFF, 0x00),
                    Color.FromRgb(0xFB, 0xFF, 0x86),
                    Color.FromRgb(0xFF, 0xFF, 0x46)
                }
            },
            { "Смешанное", new Color[]
                {
                    Color.FromRgb(0x26, 0xE8, 0x91),
                    Color.FromRgb(0x77, 0xFF, 0x00),
                    Color.FromRgb(0x00, 0xFF, 0x26),
                    Color.FromRgb(0x9F, 0xFF, 0xA3),
                    Color.FromRgb(0x91, 0xFF, 0xA9),
                    Color.FromRgb(0x1C, 0xE8, 0x8F),
                    Color.FromRgb(0x46, 0xFF, 0xA9),
                    Color.FromRgb(0x00, 0xFF, 0x6A),
                    Color.FromRgb(0x00, 0xFF, 0x6A)
                }
            },
            { "Безразличное", new Color[]
                {
                    Color.FromRgb(0x00, 0x9D, 0xFF),
                    Color.FromRgb(0x91, 0xDA, 0xFF),
                    Color.FromRgb(0x9F, 0xC9, 0xFF),
                    Color.FromRgb(0x00, 0xF6, 0xFF),
                    Color.FromRgb(0x00, 0xB4, 0xC8),
                    Color.FromRgb(0x1C, 0xC9, 0xE8),
                    Color.FromRgb(0x46, 0xCF, 0xFF),
                    Color.FromRgb(0x00, 0xD9, 0xFF),
                    Color.FromRgb(0x00, 0x9D, 0xFF)
                }
            }
        };

        public Planet()
        {
            InitializeComponent();
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

            StartGradientAnimation();
            UpdateLevelIndicators();
            Loaded += (s, e) => SetupMenuItemsAnimations();
        }
        private void SetupMenuItemsAnimations()
        {
            // Находим все MenuItem в ContextMenu
            foreach (var card in FindVisualChildren<Card>(this))
            {
                if (card.ContextMenu != null)
                {
                    foreach (var item in card.ContextMenu.Items)
                    {
                        if (item is MenuItem menuItem)
                        {
                            menuItem.MouseEnter += MenuItem_MouseEnter;
                            menuItem.MouseLeave += MenuItem_MouseLeave;
                            PrepareMenuItemAnimation(menuItem);
                        }
                    }
                }
            }
        }
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result)
                {
                    yield return result;
                }

                foreach (var descendant in FindVisualChildren<T>(child))
                {
                    yield return descendant;
                }
            }
        }

        private void PrepareMenuItemAnimation(MenuItem menuItem)
        {
            var gradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1)
            };

            // Добавляем GradientStop с цветами
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(183, 0, 255), 0));
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(0, 40, 255), 0.125));
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(70, 207, 255), 0.25));
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(81, 28, 232), 0.375));
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(170, 145, 255), 0.5));
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(78, 115, 255), 0.625));
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(210, 0, 255), 0.75));
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(0, 16, 200), 0.875));
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(136, 0, 255), 1));

            // Создаем анимации для каждого GradientStop
            var animations = new ColorAnimation[gradient.GradientStops.Count];
            for (int i = 0; i < gradient.GradientStops.Count; i++)
            {
                int nextIndex = (i + 1) % gradient.GradientStops.Count;
                animations[i] = new ColorAnimation
                {
                    From = gradient.GradientStops[i].Color,
                    To = gradient.GradientStops[nextIndex].Color,
                    Duration = TimeSpan.FromSeconds(5),
                    AutoReverse = true,
                    RepeatBehavior = RepeatBehavior.Forever
                };
            }

            menuItemAnimations[menuItem] = animations;

            // Находим Rectangle в визуальном дереве и устанавливаем градиент
            var border = FindVisualChild<Border>(menuItem);
            if (border != null)
            {
                var rect = FindVisualChild<Rectangle>(border);
                if (rect != null && rect.Name == "HoverRect")
                {
                    rect.Fill = gradient;
                }
            }
        }
        private void MenuItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItemAnimations.TryGetValue(menuItem, out var animations))
            {
                // Находим Rectangle в визуальном дереве
                var border = FindVisualChild<Border>(menuItem);
                if (border != null)
                {
                    var rect = FindVisualChild<Rectangle>(border);
                    if (rect != null && rect.Name == "HoverRect")
                    {
                        var gradient = rect.Fill as LinearGradientBrush;
                        if (gradient != null)
                        {
                            // Запускаем анимации
                            for (int i = 0; i < gradient.GradientStops.Count && i < animations.Length; i++)
                            {
                                gradient.GradientStops[i].BeginAnimation(GradientStop.ColorProperty, animations[i]);
                            }
                        }
                    }
                }
            }
        }

        private void MenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                // Находим Rectangle в визуальном дереве
                var border = FindVisualChild<Border>(menuItem);
                if (border != null)
                {
                    var rect = FindVisualChild<Rectangle>(border);
                    if (rect != null && rect.Name == "HoverRect")
                    {
                        var gradient = rect.Fill as LinearGradientBrush;
                        if (gradient != null)
                        {
                            // Останавливаем анимации
                            for (int i = 0; i < gradient.GradientStops.Count; i++)
                            {
                                gradient.GradientStops[i].BeginAnimation(GradientStop.ColorProperty, null);
                            }
                        }
                    }
                }
            }
        }

        // Вспомогательный метод для поиска дочерних элементов определенного типа
        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result)
                {
                    return result;
                }

                var descendant = FindVisualChild<T>(child);
                if (descendant != null)
                {
                    return descendant;
                }
            }

            return null;
        }

        private void StartGradientAnimation()
        {
            var gradient = (LinearGradientBrush)BackgroundRect.Fill;

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

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            // Сброс цвета фона и текста всех кнопок
            foreach (var child in ((Grid)button.Parent).Children)
            {
                if (child is Button btn)
                {
                    btn.Background = Brushes.Transparent;
                    btn.Foreground = Brushes.Gray;
                }
            }

            // Установка цвета фона и текста выбранной кнопки
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8393FE"));
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

        private void LevelScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                // Прокрутка вверх
                currentLevel = (currentLevel - 1 + 5) % 5;
            }
            else
            {
                // Прокрутка вниз
                currentLevel = (currentLevel + 1) % 5;
            }

            UpdateLevelIndicators();
            UpdateContent();
        }

        private void ChangeBackground_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                ChangeBackground(button.Content.ToString());
            }
        }

        private void ChangeBackground(string mood)
        {
            if (moodColors.TryGetValue(mood, out var colors))
            {
                var gradient = (LinearGradientBrush)BackgroundRect.Fill;

                // Останавливаем текущие анимации
                for (int i = 0; i < gradient.GradientStops.Count; i++)
                {
                    gradient.GradientStops[i].BeginAnimation(GradientStop.ColorProperty, null);
                }

                // Устанавливаем новые цвета и анимации
                for (int i = 0; i < gradient.GradientStops.Count && i < colors.Length; i++)
                {
                    gradient.GradientStops[i].Color = colors[i];

                    // Создаем новую анимацию между текущим цветом и следующим в массиве
                    int nextColorIndex = (i + 1) % colors.Length;
                    var animation = new ColorAnimation
                    {
                        From = colors[i],
                        To = colors[nextColorIndex],
                        Duration = TimeSpan.FromSeconds(10),
                        AutoReverse = true,
                        RepeatBehavior = RepeatBehavior.Forever
                    };

                    gradient.GradientStops[i].BeginAnimation(GradientStop.ColorProperty, animation);
                }
            }
        }

        private void UpdateLevelIndicators()
        {
            Level1Indicator.Width = currentLevel == 0 ? 20 : 10;
            Level1Indicator.Height = currentLevel == 0 ? 20 : 10;
            Level2Indicator.Width = currentLevel == 1 ? 20 : 10;
            Level2Indicator.Height = currentLevel == 1 ? 20 : 10;
            Level3Indicator.Width = currentLevel == 2 ? 20 : 10;
            Level3Indicator.Height = currentLevel == 2 ? 20 : 10;
            Level4Indicator.Width = currentLevel == 3 ? 20 : 10;
            Level4Indicator.Height = currentLevel == 3 ? 20 : 10;
            Level5Indicator.Width = currentLevel == 4 ? 20 : 10;
            Level5Indicator.Height = currentLevel == 4 ? 20 : 10;
        }

        private void UpdateContent()
        {
            Level1.Visibility = currentLevel == 0 ? Visibility.Visible : Visibility.Collapsed;
            Level2.Visibility = currentLevel == 1 ? Visibility.Visible : Visibility.Collapsed;
            Level3.Visibility = currentLevel == 2 ? Visibility.Visible : Visibility.Collapsed;
            Level4.Visibility = currentLevel == 3 ? Visibility.Visible : Visibility.Collapsed;
            Level5.Visibility = currentLevel == 4 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PlaylistPage());
        }
        private void OnMeretMenuItemClick(object sender, RoutedEventArgs e)
        {
            // Переход на страницу Otvet внутри этой страницы (если нужно)
            NavigationService.Navigate(new Otvet());
        }
        private void Remix_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Otvet());
        }
        private void Raspoznat_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Otvet());
        }
        private void Rasskazi_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Otvet());
        }
        private void Tune_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Otvet());
        }

        private void CreateMix_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Создание микса началось!");
        }



        private void AddToLibrary_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Трек добавлен в медиатеку!");
        }

        private void AddToQueue_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Трек добавлен в очередь!");
        }

        private void MixByArtist_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Создание микса по исполнителю началось!");
        }

        private void AddToPlaylist_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Трек добавлен в плейлист!");
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция поделиться треком!");
        }
    }
}