using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FirstTask
{
    public partial class ProfilePage : INotifyPropertyChanged
    {
        private bool isSubscribed = false;
        private int subscribersCount = 0;

        public ProfilePage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public int SubscribersCount
        {
            get { return subscribersCount; }
            set
            {
                if (subscribersCount != value)
                {
                    subscribersCount = value;
                    OnPropertyChanged();
                }
            }
        }

        private void SubscribeButton_Click(object sender, RoutedEventArgs e)
        {
            if (isSubscribed)
            {
                // Отписываемся
                SubscribeButton.Content = "Подписаться";
                SubscribersCount--;

                // Возвращаем исходную ширину и полную непрозрачность
                SubscribeButton.Width = 170;
                SubscribeButton.Opacity = 1;
                SubscribeButton.Foreground = new SolidColorBrush(Color.FromArgb(0xB3, 0xFF, 0xFF, 0xFF));
            }
            else
            {
                // Подписываемся
                SubscribeButton.Content = "Отписаться";
                SubscribersCount++;

                // Увеличиваем ширину кнопки и добавляем прозрачность
                SubscribeButton.Width = 170;
                SubscribeButton.Opacity = 1;
                SubscribeButton.Foreground = new SolidColorBrush(Color.FromArgb(0xB3, 0xFF, 0xFF, 0xFF)); // 70% прозрачности
            }

            isSubscribed = !isSubscribed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}