using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Sound_Player
{
    public partial class OknoPrivet : UserControl
    {
        public OknoPrivet()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Запускаем анимацию при загрузке
            var storyboard = (Storyboard)Resources["WelcomeAnimation"];
            storyboard.Completed += (s, args) =>
            {
                // После завершения анимации удаляем элемент
                var parent = Parent as Panel;
                parent?.Children.Remove(this);
            };
            storyboard.Begin();
        }

        // Метод для установки имени пользователя
        public void SetUserName(string userName)
        {
            var welcomeText = (TextBlock)FindName("WelcomeText");
            if (welcomeText != null)
            {
                welcomeText.Text = $"G'день, {userName}!";
            }
        }
    }
}