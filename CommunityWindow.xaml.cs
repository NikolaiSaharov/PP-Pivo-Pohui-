using CHOTOPOHOZEENASPOTIK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FirstTask
{
    public partial class CommunityWindow : Window
    {
        public CommunityWindow()
        {
            InitializeComponent();
        }

        private void skipBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.Show();
        }
    }
}
