namespace TelemetryComponent.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    public class TireControl : ProgressBar
    {
        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TireControl));
    }
}
