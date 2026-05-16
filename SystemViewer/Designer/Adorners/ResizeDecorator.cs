using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SystemViewer.Designer.Adorners
{
    public class ResizeDecorator : Control
    {
        public static readonly DependencyProperty ShowDecoratorProperty =
            DependencyProperty.Register("ShowDecorator", typeof (bool), typeof (ResizeDecorator),
                new FrameworkPropertyMetadata(false, ShowDecoratorProperty_Changed));

        private Adorner _adorner;

        public ResizeDecorator()
        {
            Unloaded += ResizeDecorator_Unloaded;
        }

        public bool ShowDecorator
        {
            get { return (bool) GetValue(ShowDecoratorProperty); }
            set { SetValue(ShowDecoratorProperty, value); }
        }

        private void HideAdorner()
        {
            if (_adorner != null)
            {
                _adorner.Visibility = Visibility.Hidden;
            }
        }

        private void ShowAdorner()
        {
            if (_adorner == null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);

                if (adornerLayer != null)
                {
                    var designerItem = DataContext as ContentControl;
                    var canvas = VisualTreeHelper.GetParent(designerItem) as Canvas;
                    _adorner = new ResizeAdorner(designerItem);
                    adornerLayer.Add(_adorner);

                    _adorner.Visibility = ShowDecorator ? Visibility.Visible : Visibility.Hidden;
                }
            }
            else
            {
                _adorner.Visibility = Visibility.Visible;
            }
        }

        private void ResizeDecorator_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_adorner != null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(_adorner);
                }

                _adorner = null;
            }
        }

        private static void ShowDecoratorProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var decorator = (ResizeDecorator) d;
            var showDecorator = (bool) e.NewValue;

            if (showDecorator)
            {
                decorator.ShowAdorner();
            }
            else
            {
                decorator.HideAdorner();
            }
        }
    }
}