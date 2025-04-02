using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using FirstTask;
using MaterialDesignThemes.Wpf;

namespace CHOTOPOHOZEENASPOTIK
{
    public partial class MainWindow : Window
    {
        private bool isRightPanelExpanded = false;
        private bool isInfoPanelExpanded = false;
        private readonly DoubleAnimation infoPanelAnimation;
        private readonly ThicknessAnimation contentMarginAnimation;
        private TranslateTransform _rightPanelTransform;

        public MainWindow()
        {
            InitializeComponent();

            // При старте сразу показываем PlanetFrame
            PlanetFrame.Visibility = Visibility.Visible;
            PlanetFrame.Navigate(new Planet());

            // Инициализация анимаций
            infoPanelAnimation = new DoubleAnimation
            {
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            contentMarginAnimation = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            // Инициализация Transform для правой панели
            _rightPanelTransform = new TranslateTransform();
            RightPanel.RenderTransform = _rightPanelTransform;

            // Устанавливаем начальное положение панели за правым краем
            Loaded += (s, e) =>
            {
                _rightPanelTransform.X = RightPanel.ActualWidth; // Скрываем за правым краем
                RightPanel.Width = 338; // Фиксируем ширину панели (можно настроить)
            };
        }
        private void SliderThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double newHeight = 120 - e.VerticalChange; // 120 - полная высота слайдера
            if (newHeight < 0) newHeight = 0;
            if (newHeight > 120) newHeight = 120;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        private void MiniPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика для открытия мини-плеера
            MessageBox.Show("Мини-плеер открыт");
        }

        private void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика для перехода в полноэкранный режим
            MessageBox.Show("Полноэкранный режим");

            // Пример реализации полноэкранного режима:
            // this.WindowState = WindowState.Maximized;
            // this.WindowStyle = WindowStyle.None;
        }
        private void NavigateBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlanetFrame.CanGoBack)
            {
                PlanetFrame.GoBack();
            }
        }

        private void NavigateForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlanetFrame.CanGoForward)
            {
                PlanetFrame.GoForward();
            }
        }


        private void ToggleInfoButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isInfoPanelExpanded) return;

            double panelWidth = 338;
            double partialOpenOffset = panelWidth - 40;

            var infoPanelAnimation = new DoubleAnimation
            {
                From = panelWidth,
                To = partialOpenOffset,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            InfoPanel.Visibility = Visibility.Visible;
            InfoPanelTransform.BeginAnimation(TranslateTransform.XProperty, infoPanelAnimation);
        }

        private void ToggleInfoButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isInfoPanelExpanded) return;

            var infoPanelAnimation = new DoubleAnimation
            {
                From = InfoPanelTransform.X,
                To = 500,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            InfoPanelTransform.BeginAnimation(TranslateTransform.XProperty, infoPanelAnimation);
        }

        public class SliderValueToHeightConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                double sliderValue = (double)value;
                double maxHeight = 120; // Максимальная высота слайдера
                return sliderValue / 100 * maxHeight; // Преобразование значения (0-100) в высоту (0-120)
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double volume = e.NewValue; // Значение от 0 до 100
            Console.WriteLine($"Громкость: {volume}");
            // Здесь можно добавить логику для управления громкостью, например:
            // mediaPlayer.Volume = volume / 100.0;
        }




        // Обработчик кнопки переключения панели справа
        private void ToggleRightPanel_Click(object sender, RoutedEventArgs e) // Переименовал для ясности
        {
            ToggleRightPanel();
        }

        private void ToggleRightPanel()
        {
            if (RightPanel == null || PlanetFrame == null) return;

            double rightPanelWidth = RightPanel.Width;

            var rightPanelAnimation = new DoubleAnimation
            {
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            var contentAnimation = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            if (!isRightPanelExpanded)
            {
                // Анимация панели
                rightPanelAnimation.From = rightPanelWidth;
                rightPanelAnimation.To = 0;
                RightPanel.Visibility = Visibility.Visible;
                _rightPanelTransform.BeginAnimation(TranslateTransform.XProperty, rightPanelAnimation);

                // Анимация контента внутри страницы
                if (PlanetFrame.Content is Planet planetPage)
                {
                    contentAnimation.From = new Thickness(0);
                    contentAnimation.To = new Thickness(0, 0, rightPanelWidth, 0);
                    planetPage.ContentContainer.BeginAnimation(FrameworkElement.MarginProperty, contentAnimation);
                }
            }
            else
            {
                // Анимация панели
                rightPanelAnimation.From = 0;
                rightPanelAnimation.To = rightPanelWidth;
                _rightPanelTransform.BeginAnimation(TranslateTransform.XProperty, rightPanelAnimation);

                // Анимация контента внутри страницы
                if (PlanetFrame.Content is Planet planetPage)
                {
                    contentAnimation.From = planetPage.ContentContainer.Margin;
                    contentAnimation.To = new Thickness(0);
                    planetPage.ContentContainer.BeginAnimation(FrameworkElement.MarginProperty, contentAnimation);
                }

                rightPanelAnimation.Completed += (s, e) => RightPanel.Visibility = Visibility.Collapsed;
            }

            isRightPanelExpanded = !isRightPanelExpanded;
        }


        // Обработчик кнопки информации
        private void ToggleInfoPanel_Click(object sender, RoutedEventArgs e)
        {
            double panelWidth = 338;

            var contentAnimation = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            if (!isInfoPanelExpanded)
            {
                // Анимация панели
                var infoPanelTranslateAnimation = new DoubleAnimation
                {
                    From = panelWidth,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.3),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                };

                InfoPanel.Visibility = Visibility.Visible;
                InfoPanelTransform.BeginAnimation(TranslateTransform.XProperty, infoPanelTranslateAnimation);

                // Анимация контента внутри страницы
                if (PlanetFrame.Content is Planet planetPage)
                {
                    contentAnimation.From = new Thickness(0);
                    contentAnimation.To = new Thickness(0, 0, panelWidth, 0);
                    planetPage.ContentContainer.BeginAnimation(FrameworkElement.MarginProperty, contentAnimation);
                }
            }
            else
            {
                // Анимация панели
                var infoPanelTranslateAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = panelWidth,
                    Duration = TimeSpan.FromSeconds(0.3),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                };

                infoPanelTranslateAnimation.Completed += (s, ev) => InfoPanel.Visibility = Visibility.Collapsed;
                InfoPanelTransform.BeginAnimation(TranslateTransform.XProperty, infoPanelTranslateAnimation);

                // Анимация контента внутри страницы
                if (PlanetFrame.Content is Planet planetPage)
                {
                    contentAnimation.From = planetPage.ContentContainer.Margin;
                    contentAnimation.To = new Thickness(0);
                    planetPage.ContentContainer.BeginAnimation(FrameworkElement.MarginProperty, contentAnimation);
                }
            }

            isInfoPanelExpanded = !isInfoPanelExpanded;
        }
        private void SetActiveButton(Button activeButton)
        {
            // Сбросить цвет всех изображений на серый
            ResetButtonColors();

            // Установить цвет активной кнопки на белый
            if (activeButton.Content is Image activeImage)
            {
                // Меняем цвет изображения на белый
                activeImage.Source = ImageColorChanger.ChangeColor(activeImage.Source, Colors.White);
            }
        }

        private void ResetButtonColors()
        {
            // Сбрасываем все изображения на серые
            if (PlanetButton.Content is Image planetImage)
            {
                planetImage.Source = ImageColorChanger.ChangeColor(planetImage.Source, Colors.Gray);
            }

            if (SamplesButton.Content is Image samplesImage)
            {
                samplesImage.Source = ImageColorChanger.ChangeColor(samplesImage.Source, Colors.Gray);
            }

            if (PersonButton.Content is Image personImage)
            {
                personImage.Source = ImageColorChanger.ChangeColor(personImage.Source, Colors.Gray);
            }
        }

        private void PlanetButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlanetFrame.Content is Planet) return;
            PlanetFrame.Visibility = Visibility.Visible;
            PlanetFrame.Navigate(new Planet());
            SetActiveButton(PlanetButton);
        }

        private void PlanetFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var frame = sender as Frame;
            if (frame != null)
            {
                frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            }
        }
        private void communityBtn_Click(object sender, RoutedEventArgs e)
        {
            CommunityWindow communityWindow = new CommunityWindow();
            Close();
            communityWindow.Show();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void PersonButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void PlaylistButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, не открыта ли уже страница профиля
            if (PlanetFrame.Content is ProfilePage) return;

            // Переходим на страницу профиля
            PlanetFrame.Visibility = Visibility.Visible;
            PlanetFrame.Navigate(new ProfilePage());

            // Закрываем правую панель, если она открыта
            if (isRightPanelExpanded)
            {
                ToggleRightPanel();
            }

            // Закрываем информационную панель, если она открыта
            if (isInfoPanelExpanded)
            {
                ToggleInfoPanel_Click(null, null);
            }
        }
        private void VolumeButton_Click(object sender, RoutedEventArgs e)
        {
            VolumePopup.IsOpen = !VolumePopup.IsOpen;
        }

        private void SamplesButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlanetFrame.Content is Samples) return;
            PlanetFrame.Navigate(new Samples());
            SetActiveButton(SamplesButton);
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика для кнопки уведомлений
        }

        private void CommunityButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика для кнопки "Вступить в Community"
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#171717"));
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.Black;
            }
        }
        private void OnMeretMenuItemClick(object sender, RoutedEventArgs e)
        {
            // Закрыть текущую страницу (если нужно)
            PlanetFrame.Visibility = Visibility.Collapsed;

            // Навигация на страницу Otvet
            PlanetFrame.Navigate(new Otvet());
        }
        private void ScreenButton_Click(object sender, RoutedEventArgs e)
        {
            ScreenContextMenu.PlacementTarget = ScreenButton;
            ScreenContextMenu.IsOpen = true;
            e.Handled = true; // Предотвращаем всплытие события
        }
    }
}