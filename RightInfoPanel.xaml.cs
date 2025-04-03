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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FirstTask
{
    /// <summary>
    /// Логика взаимодействия для RightInfoPanel.xaml
    /// </summary>
    public partial class RightInfoPanel : Page
    {
        public RightInfoPanel()
        {
            InitializeComponent();
        }

        // Метод для поиска дочерних элементов определенного типа
        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                
                if (child != null && child is T)
                    yield return (T)child;

                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        // Метод для обновления информации о треке
        public void UpdateTrackInfo(string title, string artist, string album, string duration)
        {
            // Найти TextBlock'и по имени и обновить их текст
            var textBlocks = FindVisualChildren<TextBlock>(this);
            foreach (var textBlock in textBlocks)
            {
                switch (textBlock.Text)
                {
                    case "Sample Track":
                        textBlock.Text = title;
                        break;
                    case "Sample Artist":
                        textBlock.Text = artist;
                        break;
                    case "Sample Album":
                        textBlock.Text = album;
                        break;
                    case "3:45":
                        textBlock.Text = duration;
                        break;
                }
            }
        }
    }
}
