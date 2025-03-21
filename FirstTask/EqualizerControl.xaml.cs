using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace FirstTask
{
    public partial class EqualizerControl : UserControl
    {
        public EqualizerControl()
        {
            InitializeComponent();
            StartAnimation();
        }

        private void StartAnimation()
        {
            // Анимация для каждой полоски
            CreateRandomHeightAnimation(Bar1, TimeSpan.FromMilliseconds(200));
            CreateRandomHeightAnimation(Bar2, TimeSpan.FromMilliseconds(300));
            CreateRandomHeightAnimation(Bar3, TimeSpan.FromMilliseconds(400));
            CreateRandomHeightAnimation(Bar4, TimeSpan.FromMilliseconds(500));
            CreateRandomHeightAnimation(Bar5, TimeSpan.FromMilliseconds(600));
        }

        private void CreateRandomHeightAnimation(FrameworkElement element, TimeSpan duration)
        {
            Random random = new Random();

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 20, // Минимальная высота
                To = 100,  // Максимальная высота
                Duration = duration,
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            // Случайное изменение высоты
            animation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut };

            element.BeginAnimation(FrameworkElement.HeightProperty, animation);
        }
    }
}
