using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class TrackControl : UserControl
    {
        public event EventHandler<TrackControl> OnTrackClicked;
        public TrackControl()
        {
            InitializeComponent();
        }

        private void TrackBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OnTrackClicked?.Invoke(this, this);
        }

        public string TrackNumber
        {
            get => tbTrackNumber.Text;
            set => tbTrackNumber.Text = value;
        }

        public string TrackTitle
        {
            get => tbTrackTitle.Text;
            set => tbTrackTitle.Text = value;
        }

        public string TrackArtist
        {
            get => tbTrackArtist.Text;
            set => tbTrackArtist.Text = value;
        }

        public string TrackDuration
        {
            get => tbTrackDuration.Text;
            set => tbTrackDuration.Text = value;
        }

        public string TrackImageSource
        {
            get => TrackImage.Source.ToString();
            set => TrackImage.Source = new System.Windows.Media.Imaging.BitmapImage(new System.Uri(value, System.UriKind.RelativeOrAbsolute));
        }
    }

}
