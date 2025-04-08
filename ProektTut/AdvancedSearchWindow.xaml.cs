using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sound_Player
{
    /// <summary>
    /// Логика взаимодействия для AdvancedSearchWindow.xaml
    /// </summary>
    public partial class AdvancedSearchWindow : Window
    {
        private Window _mainWindow;
        private Page AdvancedSearch;

        public AdvancedSearchWindow(Window mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            AdvancedSearch = new AdvancedSearchPage();

            UpdatePositionAndSize();

            // Подписка на изменения основного окна
            _mainWindow.LocationChanged += (s, e) => UpdatePositionAndSize();
            _mainWindow.SizeChanged += (s, e) => UpdatePositionAndSize();
            _mainWindow.StateChanged += (s, e) => UpdatePositionAndSize();
        }
        private void UpdatePositionAndSize()
        {
            this.Left = _mainWindow.Left;
            this.Top = _mainWindow.Top;
            this.Width = _mainWindow.Width;
            this.Height = _mainWindow.Height;

            // Если основное окно развёрнуто, тоже разворачиваем AdvancedSearch
            this.WindowState = _mainWindow.WindowState;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Hide();
                SearchTextBox_LostFocus(sender, e);
            }
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            SearchTextBox_LostFocus(sender, e);// Закрытие при клике по фону
        }
        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // При получении фокуса отображаем Page
            AdvancedTrackPanel.Content = AdvancedSearch;
            AdvancedTrackPanel.Visibility = Visibility.Visible;
            AnimateFrame();
        }
        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // При потере фокуса скрываем Page
            AdvancedTrackPanel.Visibility = Visibility.Collapsed;
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
            // Создаем анимацию для свойства Height
            DoubleAnimation heightAnimation = new DoubleAnimation
            {
                From = 0,
                To = 600,
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };

            // Создаем Storyboard и добавляем анимацию
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(heightAnimation);
            Storyboard.SetTarget(heightAnimation, AdvancedTrackPanel);
            Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(FrameworkElement.HeightProperty));

            // Запускаем анимацию
            storyboard.Begin();
        }
    }
}
