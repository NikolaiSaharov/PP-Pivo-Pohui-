using System.Windows.Controls;


namespace FirstTask
{
    public partial class TrackArtistControl : UserControl
    {
        public TrackArtistControl()
        {
            InitializeComponent();
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

        public string TrackImageSource
        {
            get => TrackImage.Source.ToString();
            set => TrackImage.Source = new System.Windows.Media.Imaging.BitmapImage(new System.Uri(value, System.UriKind.RelativeOrAbsolute));
        }
    }
}
