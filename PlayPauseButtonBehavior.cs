using MaterialDesignThemes.Wpf;
using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows;

namespace FirstTask
{
    public class PlayPauseButtonBehavior : Behavior<Button>
    {
        public static readonly DependencyProperty IsPlayingProperty =
            DependencyProperty.Register("IsPlaying", typeof(bool), typeof(PlayPauseButtonBehavior),
                new PropertyMetadata(false, OnIsPlayingChanged));

        public bool IsPlaying
        {
            get => (bool)GetValue(IsPlayingProperty);
            set => SetValue(IsPlayingProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += OnButtonClick;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= OnButtonClick;
            base.OnDetaching();
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            IsPlaying = !IsPlaying;
        }

        private static void OnIsPlayingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = (PlayPauseButtonBehavior)d;
            behavior.UpdateIcon();
        }

        private void UpdateIcon()
        {
            if (AssociatedObject.Content is PackIcon icon)
            {
                icon.Kind = IsPlaying ? PackIconKind.Pause : PackIconKind.Play;
            }
        }
    }
}