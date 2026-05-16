using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SystemViewer.Designer.Adorners
{
    public class ResizeAdorner : Adorner
    {
        private readonly ResizeChrome _chrome;
        private readonly VisualCollection _visuals;

        public ResizeAdorner(ContentControl designerItem)
            : base(designerItem)
        {
            _chrome = new ResizeChrome();
            _visuals = new VisualCollection(this);
            _visuals.Add(_chrome);
            _chrome.DataContext = designerItem;
        }

        protected override int VisualChildrenCount
        {
            get { return _visuals.Count; }
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            _chrome.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visuals[index];
        }
    }
}