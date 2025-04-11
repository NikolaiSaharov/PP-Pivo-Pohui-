using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using MaterialDesignThemes.Wpf;
using SharpVectors.Converters;

namespace Sound_Player
{
    public partial class MainWindow : Window
    {
        private bool isRightPanelExpanded = false;
        private bool isInfoPanelExpanded = false;
        private readonly DoubleAnimation infoPanelAnimation;
        private readonly ThicknessAnimation contentMarginAnimation;
        private TranslateTransform _rightPanelTransform;
        private Page HistoryPage;
        private AdvancedSearchWindow _advancedSearchWindow;

        public MainWindow()
        {
            InitializeComponent();

            // Запускаем анимацию после загрузки окна
            Loaded += (s, e) =>
            {
                var storyboard = (Storyboard)FindResource("MainWindowShowAnimation");
                storyboard.Begin();
            };

            HistoryPage = new HistoryOfSearchPage();
            this.Loaded += MainWindow_Loaded;

            PlanetFrame.Visibility = Visibility.Visible;
            PlanetFrame.Navigate(new Planet());

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

            _rightPanelTransform = new TranslateTransform();
            RightPanel.RenderTransform = _rightPanelTransform;

            Loaded += (s, e) =>
            {
                _rightPanelTransform.X = RightPanel.ActualWidth;
                RightPanel.Width = 338;
            };

            LoadSvgImage("/Resources/Images/Logotype_White.svg");
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _advancedSearchWindow = new AdvancedSearchWindow(this);

            MainContentGrid.Opacity = 0; // Изначально скрыт
            var blurEffect = MainContentGrid.Effect as BlurEffect;
            if (blurEffect == null)
            {
                blurEffect = new BlurEffect();
                MainContentGrid.Effect = blurEffect;
            }
            
            blurEffect.Radius = 10; // Изначальное размытие

            var welcomeWindow = new OknoPrivet();
            welcomeWindow.SetUserName("User Name");
            OverlayGrid.Children.Add(welcomeWindow);

            var storyboard = (Storyboard)welcomeWindow.Resources["WelcomeAnimation"];  
            storyboard.Completed += (s, args) =>
            {
                OverlayBackground.Visibility = Visibility.Collapsed;

                // Анимация появления главного экрана
                var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.7));
                var blurOut = new DoubleAnimation(10, 0, TimeSpan.FromSeconds(0.7));

                MainContentGrid.BeginAnimation(UIElement.OpacityProperty, fadeIn);
                blurEffect.BeginAnimation(BlurEffect.RadiusProperty, blurOut);
            };
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _advancedSearchWindow?.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.K && Keyboard.Modifiers == ModifierKeys.Control)
            {
                ToggleAdvancedSearch();
            }
        }

        private void ToggleAdvancedSearch()
        {
            if (_advancedSearchWindow.IsVisible)
                _advancedSearchWindow.Hide();
            else
                _advancedSearchWindow.Show();
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            History.Content = HistoryPage;
            History.Visibility = Visibility.Visible;
            AnimateFrame();
            LoadSvgImage("/Resources/Images/Logotype_Blue.svg");
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            History.Visibility = Visibility.Collapsed;
            LoadSvgImage("/Resources/Images/Logotype_White.svg");
        }

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.IsKeyboardFocused)
            {
                Keyboard.ClearFocus();
                e.Handled = true;
                SearchTextBox_LostFocus(sender, e);
            }
            else
            {
                e.Handled = false;
                SearchTextBox_GotFocus(sender, e);
            }
        }

        private void AnimateFrame()
        {
            DoubleAnimation heightAnimation = new DoubleAnimation
            {
                From = 0,
                To = 450,
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(heightAnimation);
            Storyboard.SetTarget(heightAnimation, History);
            Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(FrameworkElement.HeightProperty));
            storyboard.Begin();
        }

        private void LoadSvgImage(string path)
        {
            var svgImage = new SvgViewbox
            {
                Source = new Uri(path, UriKind.RelativeOrAbsolute)
            };
            SvgImage.Source = svgImage.Source;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void SliderThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double newHeight = 120 - e.VerticalChange;
            if (newHeight < 0) newHeight = 0;
            if (newHeight > 120) newHeight = 120;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private void MiniPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Мини-плеер открыт");
        }

        private void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            if (Fullscreen.Visibility == Visibility.Collapsed)
            {
                // Создаем и настраиваем FullPlayerPage
                var fullPlayerPage = new FullPlayerPage();
                fullPlayerPage.CloseRequested += (s, args) =>
                {
                    // Скрываем Fullscreen Frame
                    Fullscreen.Visibility = Visibility.Collapsed;
                    Fullscreen.Content = null;
                    
                    // Показываем основной контент
                    PlanetFrame.Visibility = Visibility.Visible;
                };

                // Показываем Fullscreen Frame и загружаем FullPlayerPage
                Fullscreen.Navigate(fullPlayerPage);
                Fullscreen.Visibility = Visibility.Visible;

                // Скрываем основное содержимое PlanetFrame, чтобы избежать наложения
                PlanetFrame.Visibility = Visibility.Collapsed;

                // Если боковые панели открыты, закрываем их
                if (isRightPanelExpanded)
                {
                    ToggleRightPanel();
                }
                if (isInfoPanelExpanded)
                {
                    ToggleInfoPanel_Click(null, null);
                }
            }
            else
            {
                // Скрываем Fullscreen Frame и возвращаем основное содержимое
                Fullscreen.Visibility = Visibility.Collapsed;
                Fullscreen.Content = null; // Очищаем содержимое для экономии ресурсов
                PlanetFrame.Visibility = Visibility.Visible;
            }
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

            if (InfoPanel.Content == null)
            {
                InfoPanel.Navigate(new RightInfoPanel());
            }
            InfoPanel.Visibility = Visibility.Visible;
            InfoPanelTransform.BeginAnimation(TranslateTransform.XProperty, infoPanelAnimation);
        }

        private void ToggleInfoButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isInfoPanelExpanded) return;

            var infoPanelAnimation = new DoubleAnimation
            {
                From = InfoPanelTransform.X,
                To = 338,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            infoPanelAnimation.Completed += (s, ev) =>
            {
                if (!isInfoPanelExpanded)
                {
                    InfoPanel.Visibility = Visibility.Collapsed;
                    InfoPanel.Content = null;
                }
            };
            InfoPanelTransform.BeginAnimation(TranslateTransform.XProperty, infoPanelAnimation);
        }

        public class SliderValueToHeightConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                double sliderValue = (double)value;
                double maxHeight = 120;
                return sliderValue / 100 * maxHeight;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double volume = e.NewValue;
            Console.WriteLine($"Громкость: {volume}");
        }

        private void ToggleRightPanel_Click(object sender, RoutedEventArgs e)
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
                rightPanelAnimation.From = rightPanelWidth;
                rightPanelAnimation.To = 0;
                RightPanel.Visibility = Visibility.Visible;
                _rightPanelTransform.BeginAnimation(TranslateTransform.XProperty, rightPanelAnimation);

                if (PlanetFrame.Content is Planet planetPage)
                {
                    contentAnimation.From = new Thickness(0);
                    contentAnimation.To = new Thickness(0, 0, rightPanelWidth, 0);
                    planetPage.ContentContainer.BeginAnimation(FrameworkElement.MarginProperty, contentAnimation);
                }
            }
            else
            {
                rightPanelAnimation.From = 0;
                rightPanelAnimation.To = rightPanelWidth;
                _rightPanelTransform.BeginAnimation(TranslateTransform.XProperty, rightPanelAnimation);

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
        public void HideFullscreenFrame()
        {
            Fullscreen.Visibility = Visibility.Collapsed;
            Fullscreen.Content = null; // Очищаем содержимое для экономии ресурсов
            PlanetFrame.Visibility = Visibility.Visible;
        }

        private void ToggleInfoPanel_Click(object sender, RoutedEventArgs e)
        {
            double panelWidth = 338;

            // Анимация для отступа справа у PlanetBorder
            var borderMarginAnimation = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            // Анимация для InfoPanel
            var infoPanelTranslateAnimation = new DoubleAnimation
            {
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            if (!isInfoPanelExpanded)
            {
                InfoPanel.Navigate(new RightInfoPanel());
                InfoPanel.Visibility = Visibility.Visible;

                // Анимация панели (сдвиг с правого края)
                infoPanelTranslateAnimation.From = panelWidth;
                infoPanelTranslateAnimation.To = 0;
                InfoPanelTransform.BeginAnimation(TranslateTransform.XProperty, infoPanelTranslateAnimation);

                // Увеличиваем правый отступ PlanetBorder, чтобы сжать его справа
                borderMarginAnimation.From = PlanetBorder.Margin; // Текущий отступ (0,0,10,0)
                borderMarginAnimation.To = new Thickness(0, 0, panelWidth + 10, 0); // Добавляем panelWidth к существующему Margin.Right
                PlanetBorder.BeginAnimation(Border.MarginProperty, borderMarginAnimation);
            }
            else
            {
                // Анимация панели (сдвиг обратно вправо)
                infoPanelTranslateAnimation.From = 0;
                infoPanelTranslateAnimation.To = panelWidth;
                infoPanelTranslateAnimation.Completed += (s, ev) =>
                {
                    InfoPanel.Visibility = Visibility.Collapsed;
                    InfoPanel.Content = null;
                };
                InfoPanelTransform.BeginAnimation(TranslateTransform.XProperty, infoPanelTranslateAnimation);

                // Возвращаем правый отступ к исходному (10)
                borderMarginAnimation.From = PlanetBorder.Margin;
                borderMarginAnimation.To = new Thickness(0, 0, 10, 0); // Исходный Margin.Right = 10
                PlanetBorder.BeginAnimation(Border.MarginProperty, borderMarginAnimation);
            }

            isInfoPanelExpanded = !isInfoPanelExpanded;
        }

        private void SetActiveButton(Button activeButton)
        {
            ResetButtonColors();

            if (activeButton.Content is Image activeImage)
            {
                activeImage.Source = ImageColorChanger.ChangeColor(activeImage.Source, Colors.White);
            }
        }

        private void ResetButtonColors()
        {
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
            if (PlanetFrame.Content is ProfilePage) return;

            PlanetFrame.Visibility = Visibility.Visible;
            PlanetFrame.Navigate(new ProfilePage());

            if (isRightPanelExpanded)
            {
                ToggleRightPanel();
            }

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
        }

        private void CommunityButton_Click(object sender, RoutedEventArgs e)
        {
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
            PlanetFrame.Visibility = Visibility.Collapsed;
            PlanetFrame.Navigate(new Otvet());
        }

        private void ScreenButton_Click(object sender, RoutedEventArgs e)
        {
            ScreenContextMenu.PlacementTarget = ScreenButton;
            ScreenContextMenu.IsOpen = true;
            e.Handled = true;
        }
    }
}
