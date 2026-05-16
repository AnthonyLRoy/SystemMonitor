using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SystemViewer.Designer.Adorners
{
    public class SizeAdorner : Adorner
    {
        private readonly SizeChrome chrome;
        private readonly VisualCollection visuals;
        private ContentControl designerItem;

        public SizeAdorner(ContentControl designerItem)
            : base(designerItem)
        {
            SnapsToDevicePixels = true;
            this.designerItem = designerItem;
            chrome = new SizeChrome {DataContext = designerItem};
            visuals = new VisualCollection(this) {chrome};
        }

        protected override int VisualChildrenCount
        {
            get { return visuals.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            chrome.Arrange(new Rect(new Point(0.0, 0.0), arrangeBounds));
            return arrangeBounds;
        }
    }
}