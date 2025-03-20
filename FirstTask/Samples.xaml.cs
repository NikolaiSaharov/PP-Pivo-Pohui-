using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CHOTOPOHOZEENASPOTIK
{
    public partial class Samples : Page
    {
        private TranslateTransform _translateTransform;
        private double _maxOffset; // Максимальное смещение (конец списка)

        public Samples()
        {
            InitializeComponent();
            _translateTransform = new TranslateTransform();
            CardStackPanel.RenderTransform = _translateTransform;

            // Вычисляем максимальное смещение после загрузки содержимого
            Loaded += (s, e) =>
            {
                _maxOffset = Math.Max(0, CardStackPanel.ActualHeight - MainScrollViewer.ActualHeight);
                UpdateButtonVisibility(); // Обновляем видимость кнопок при загрузке
            };

            // Блокируем прокрутку колесиком мыши
            MainScrollViewer.MouseWheel += (s, e) => e.Handled = true;
        }

        private void DownArrowButton_Click(object sender, RoutedEventArgs e)
        {
            ScrollToNextCard(-1); // Прокрутка вниз (визуально вверх)
        }

        private void UpArrowButton_Click(object sender, RoutedEventArgs e)
        {
            ScrollToNextCard(1); // Прокрутка вверх (визуально вниз)
        }

        // Универсальный метод для прокрутки на следующую/предыдущую карточку
        private void ScrollToNextCard(int direction)
        {
            double cardHeight = FirstCard.ActualHeight + 35; // Высота карточки с отступом
            double currentPosition = _translateTransform.Y;
            double targetOffset;

            if (direction < 0) // Прокрутка вниз (визуально вверх)
            {
                targetOffset = currentPosition - cardHeight;
                if (Math.Abs(targetOffset) >= _maxOffset - cardHeight / 2)
                {
                    targetOffset = -_maxOffset; // Принудительно к концу
                }
                else
                {
                    targetOffset = Math.Max(targetOffset, -_maxOffset); // Ограничиваем
                }
            }
            else // Прокрутка вверх (визуально вниз)
            {
                targetOffset = currentPosition + cardHeight;
                if (targetOffset >= -cardHeight / 2)
                {
                    targetOffset = 0; // Принудительно к началу
                }
                else
                {
                    targetOffset = Math.Min(targetOffset, 0); // Ограничиваем
                }
            }

            AnimateScroll(targetOffset);
        }

        private void AnimateScroll(double targetOffset)
        {
            DoubleAnimation scrollAnimation = new DoubleAnimation
            {
                From = _translateTransform.Y,
                To = targetOffset,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            // Обновляем видимость кнопок после завершения анимации
            scrollAnimation.Completed += (s, e) => UpdateButtonVisibility();

            _translateTransform.BeginAnimation(TranslateTransform.YProperty, scrollAnimation);
        }

        // Метод для обновления видимости кнопок
        private void UpdateButtonVisibility()
        {
            const double tolerance = 0.1; // Допуск для сравнения

            if (Math.Abs(_translateTransform.Y) < tolerance) // На первой карточке
            {
                DownArrowButton.Visibility = Visibility.Visible;
                UpArrowButton.Visibility = Visibility.Collapsed;
            }
            else if (Math.Abs(_translateTransform.Y + _maxOffset) < tolerance) // На последней карточке
            {
                DownArrowButton.Visibility = Visibility.Collapsed;
                UpArrowButton.Visibility = Visibility.Visible;
            }
            else // На средней карточке
            {
                DownArrowButton.Visibility = Visibility.Visible;
                UpArrowButton.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Логика для кнопки (если есть)
        }
    }
}