using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls.Primitives;
using FirstTask;
using System.Windows.Navigation;

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
                RightPanel.Width = 300; // Фиксируем ширину панели (можно настроить)
            };
        }

        // Обработчик кнопки переключения панели справа
        private void ToggleRightPanel_Click(object sender, RoutedEventArgs e) // Переименовал для ясности
        {
            ToggleRightPanel();
        }

        private void ToggleRightPanel()
        {
            if (RightPanel == null || PlanetFrame == null) return;

            double rightPanelWidth = RightPanel.Width; // Используем фиксированную ширину

            var rightPanelAnimation = new DoubleAnimation
            {
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            var frameMarginAnimation = new ThicknessAnimation
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

                frameMarginAnimation.From = PlanetFrame.Margin;
                frameMarginAnimation.To = new Thickness(0, 0, rightPanelWidth, 0);
                PlanetFrame.BeginAnimation(FrameworkElement.MarginProperty, frameMarginAnimation);
            }
            else
            {
                rightPanelAnimation.From = 0;
                rightPanelAnimation.To = rightPanelWidth;
                _rightPanelTransform.BeginAnimation(TranslateTransform.XProperty, rightPanelAnimation);

                frameMarginAnimation.From = PlanetFrame.Margin;
                frameMarginAnimation.To = new Thickness(0);
                PlanetFrame.BeginAnimation(FrameworkElement.MarginProperty, frameMarginAnimation);

                rightPanelAnimation.Completed += (s, e) => RightPanel.Visibility = Visibility.Collapsed;
            }

            isRightPanelExpanded = !isRightPanelExpanded;
        }

        // Обработчик кнопки информации
        private void ToggleInfoPanel_Click(object sender, RoutedEventArgs e)
        {
            double panelWidth = 385;

            if (!isInfoPanelExpanded)
            {
                infoPanelAnimation.From = 0;
                infoPanelAnimation.To = panelWidth;

                contentMarginAnimation.From = PlanetFrame.Margin;
                contentMarginAnimation.To = new Thickness(0, 0, panelWidth, 0);

                var icon = ToggleInfoButton.Content as PackIcon;
                if (icon != null) icon.Kind = PackIconKind.ChevronRight;

                InfoPanel.Visibility = Visibility.Visible;
                InfoPanel.BeginAnimation(FrameworkElement.WidthProperty, infoPanelAnimation);
                PlanetFrame.BeginAnimation(FrameworkElement.MarginProperty, contentMarginAnimation);
            }
            else
            {
                infoPanelAnimation.From = panelWidth;
                infoPanelAnimation.To = 0;

                contentMarginAnimation.From = PlanetFrame.Margin;
                contentMarginAnimation.To = new Thickness(0);

                var icon = ToggleInfoButton.Content as PackIcon;
                if (icon != null) icon.Kind = PackIconKind.ChevronLeft;

                InfoPanel.BeginAnimation(FrameworkElement.WidthProperty, infoPanelAnimation);
                PlanetFrame.BeginAnimation(FrameworkElement.MarginProperty, contentMarginAnimation);
            }

            isInfoPanelExpanded = !isInfoPanelExpanded;
        }

        private void PlanetButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlanetFrame.Content is Planet) return;
            PlanetFrame.Visibility = Visibility.Visible;
            PlanetFrame.Navigate(new Planet());
        }

        private void PlanetFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var frame = sender as Frame;
            if (frame != null)
            {
                frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            }
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

        private void SamplesButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlanetFrame.Content is Samples) return;
            PlanetFrame.Navigate(new Samples());
        }

        private void communityBtn_Click(object sender, RoutedEventArgs e)
        {
            CommunityWindow communityWindow = new CommunityWindow();
            Close();
            communityWindow.Show(); 
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
