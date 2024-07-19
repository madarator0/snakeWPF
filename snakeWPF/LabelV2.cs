using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace snakeWPF
{
    public class LabelV2 : Label
    {
        public XY XY { get; set; }

        public LabelV2()
        {
            BorderThickness = new Thickness(1);
            BorderBrush = Brushes.Black;
            Margin = new Thickness(1);
        }

        public void SetRoundedCorners(double radius)
        {
            CornerRadius = new CornerRadius(radius);
            Background = Brushes.Green;
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LabelV2), new PropertyMetadata(new CornerRadius()));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
    }
}
