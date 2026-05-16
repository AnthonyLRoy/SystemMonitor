using System.Windows;
using System.Windows.Controls;

namespace SystemViewer.Designer.Adorners
{
    public class ResizeChrome : Control
    {
        static ResizeChrome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeChrome), new FrameworkPropertyMetadata(typeof(ResizeChrome)));
        }
    }
}
