using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FirstTask
{
    public partial class TrackControl : UserControl
    {
        public event EventHandler<TrackControl> OnTrackClicked;

        public TrackControl()
        {
            InitializeComponent();
            TrackBorder.MouseEnter += (s, e) =>
            {
                if (!IsSelected)
                {
                    TrackBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#090909"));
                }
            };

            TrackBorder.MouseLeave += (s, e) =>
            {
                if (!IsSelected)
                {
                    TrackBorder.Background = Brushes.Transparent;
                }
            };
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                TrackBorder.Background = value
                    ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#090909"))
                    : Brushes.Transparent;
            }
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