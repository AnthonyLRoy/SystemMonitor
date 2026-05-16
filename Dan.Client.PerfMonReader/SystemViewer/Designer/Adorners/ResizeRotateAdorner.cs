using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SystemViewer.Designer.Adorners
{
    public class ResizeRotateAdorner : Adorner
    {
        private readonly ResizeRotateChrome chrome;
        private readonly VisualCollection visuals;

        public ResizeRotateAdorner(ContentControl designerItem)
            : base(designerItem)
        {
            SnapsToDevicePixels = true;
            chrome = new ResizeRotateChrome {DataContext = designerItem};
            visuals = new VisualCollection(this) {chrome};
        }

        protected override int VisualChildrenCount
        {
            get { return visuals.Count; }
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            chrome.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }
    }
}