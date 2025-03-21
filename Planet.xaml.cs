using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace CHOTOPOHOZEENASPOTIK
{
    public partial class Planet : Page
    {
        private readonly ColorAnimation[] gradientAnimations;

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
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9BC2DB"));
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}