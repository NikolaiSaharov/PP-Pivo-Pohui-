using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FirstTask
{
    public partial class ProfilePage
    {
        private bool isSubscribed = false;
        private int subscribersCount = 0;

        public ProfilePage()
        {
            InitializeComponent();
        }

        private void SubscribeButton_Click(object sender, RoutedEventArgs e)
        {
            if (isSubscribed)
            {
                // Отписываемся
                SubscribeButton.Content = "Подписаться";
                subscribersCount--;

                // Возвращаем исходную ширину и полную непрозрачность
                SubscribeButton.Width = 170;
                SubscribeButton.Opacity = 1;
                SubscribeButton.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                // Подписываемся
                SubscribeButton.Content = "Вы подписаны";
                subscribersCount++;

                // Увеличиваем ширину кнопки и добавляем прозрачность
                SubscribeButton.Width = 190;
                SubscribeButton.Opacity = 0.7;
                SubscribeButton.Foreground = new SolidColorBrush(Color.FromArgb(0xB3, 0xFF, 0xFF, 0xFF)); // 70% прозрачности
            }

            isSubscribed = !isSubscribed;
        }
    }
}