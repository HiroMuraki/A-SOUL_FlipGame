using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ASGame_FlipGame.View {
    public class RSlider : Slider {
        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.Register(nameof(Theme), typeof(Brush), typeof(RSlider), new PropertyMetadata(null));
        public Brush Theme {
            get { return (Brush)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        static RSlider() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RSlider), new FrameworkPropertyMetadata(typeof(RSlider)));
        }
    }
}
