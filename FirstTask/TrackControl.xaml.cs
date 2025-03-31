using System;
using System.Windows.Controls;
using System.Windows.Input;


namespace FirstTask
{
    public partial class TrackControl : UserControl
    {
        public event EventHandler<TrackControl> OnTrackClicked;
        public event EventHandler ArtistClicked;
        public TrackControl()
        {
            InitializeComponent();
        }

        private void tbTrackArtist_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ArtistClicked?.Invoke(this, EventArgs.Empty);
            e.Handled = true;
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
