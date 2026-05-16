// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagramCanvas.Command.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The diagram canvas.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Xml;
    using System.Xml.Linq;

    using SystemViewer.Designer;
    using SystemViewer.Forms;

    using Microsoft.Win32;

    /// <summary>
    /// The diagram canvas.
    /// </summary>
    public partial class DiagramCanvas
    {
        #region Static Fields

        /// <summary>
        /// The align bottom.
        /// </summary>
        public static RoutedCommand AlignBottom = new RoutedCommand();

        /// <summary>
        /// The align horizontal centers.
        /// </summary>
        public static RoutedCommand AlignHorizontalCenters = new RoutedCommand();

        /// <summary>
        /// The align left.
        /// </summary>
        public static RoutedCommand AlignLeft = new RoutedCommand();

        /// <summary>
        /// The align right.
        /// </summary>
        public static RoutedCommand AlignRight = new RoutedCommand();

        /// <summary>
        /// The align top.
        /// </summary>
        public static RoutedCommand AlignTop = new RoutedCommand();

        /// <summary>
        /// The align vertical centers.
        /// </summary>
        public static RoutedCommand AlignVerticalCenters = new RoutedCommand();

        /// <summary>
        /// The bring forward.
        /// </summary>
        public static RoutedCommand BringForward = new RoutedCommand();

        /// <summary>
        /// The bring to front.
        /// </summary>
        public static RoutedCommand BringToFront = new RoutedCommand();

        /// <summary>
        /// The distribute horizontal.
        /// </summary>
        public static RoutedCommand DistributeHorizontal = new RoutedCommand();

        /// <summary>
        /// The distribute vertical.
        /// </summary>
        public static RoutedCommand DistributeVertical = new RoutedCommand();

        /// <summary>
        /// The group.
        /// </summary>
        public static RoutedCommand Group = new RoutedCommand();

        /// <summary>
        /// The select all.
        /// </summary>
        public static RoutedCommand SelectAll = new RoutedCommand();

        /// <summary>
        /// The send backward.
        /// </summary>
        public static RoutedCommand SendBackward = new RoutedCommand();

        /// <summary>
        /// The send to back.
        /// </summary>
        public static RoutedCommand SendToBack = new RoutedCommand();

        /// <summary>
        /// The ungroup.
        /// </summary>
        public static RoutedCommand Ungroup = new RoutedCommand();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The prepare commands.
        /// </summary>
        public void PrepareCommands()
        {
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.New, this.New_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, this.Open_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, this.Save_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, this.Print_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, this.Cut_Executed, this.Cut_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(ApplicationCommands.Copy, this.Copy_Executed, this.Copy_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(ApplicationCommands.Paste, this.Paste_Executed, this.Paste_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(ApplicationCommands.Delete, this.Delete_Executed, this.Delete_Enabled));
            this.CommandBindings.Add(new CommandBinding(DiagramCanvas.Group, this.Group_Executed, this.Group_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(DiagramCanvas.Ungroup, this.Ungroup_Executed, this.Ungroup_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(DiagramCanvas.BringForward, this.BringForward_Executed, this.Order_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(DiagramCanvas.BringToFront, this.BringToFront_Executed, this.Order_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(DiagramCanvas.SendBackward, this.SendBackward_Executed, this.Order_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(DiagramCanvas.SendToBack, this.SendToBack_Executed, this.Order_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(DiagramCanvas.AlignTop, this.AlignTop_Executed, this.Align_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(
                    DiagramCanvas.AlignVerticalCenters, 
                    this.AlignVerticalCenters_Executed, 
                    this.Align_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(DiagramCanvas.AlignBottom, this.AlignBottom_Executed, this.Align_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(DiagramCanvas.AlignLeft, this.AlignLeft_Executed, this.Align_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(
                    DiagramCanvas.AlignHorizontalCenters, 
                    this.AlignHorizontalCenters_Executed, 
                    this.Align_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(DiagramCanvas.AlignRight, this.AlignRight_Executed, this.Align_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(
                    DiagramCanvas.DistributeHorizontal, 
                    this.DistributeHorizontal_Executed, 
                    this.Distribute_Enabled));
            this.CommandBindings.Add(
                new CommandBinding(
                    DiagramCanvas.DistributeVertical, 
                    this.DistributeVertical_Executed, 
                    this.Distribute_Enabled));
            this.CommandBindings.Add(new CommandBinding(DiagramCanvas.SelectAll, this.SelectAll_Executed));
            SelectAll.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));

            this.AllowDrop = true;
            Clipboard.Clear();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The deserialize designer item.
        /// </summary>
        /// <param name="itemXML">
        /// The item xml.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="OffsetX">
        /// The offset x.
        /// </param>
        /// <param name="OffsetY">
        /// The offset y.
        /// </param>
        /// <returns>
        /// The <see cref="DesignerItem"/>.
        /// </returns>
        private static DesignerItem DeserializeDesignerItem(XElement itemXML, Guid id, double OffsetX, double OffsetY)
        {
            var item = new DesignerItem(id);
            item.Width = double.Parse(itemXML.Element("Width").Value, CultureInfo.InvariantCulture);
            item.Height = double.Parse(itemXML.Element("Height").Value, CultureInfo.InvariantCulture);
            item.ParentID = new Guid(itemXML.Element("ParentID").Value);
            item.IsGroup = bool.Parse(itemXML.Element("IsGroup").Value);
            Canvas.SetLeft(item, double.Parse(itemXML.Element("Left").Value, CultureInfo.InvariantCulture) + OffsetX);
            Canvas.SetTop(item, double.Parse(itemXML.Element("Top").Value, CultureInfo.InvariantCulture) + OffsetY);
            Panel.SetZIndex(item, int.Parse(itemXML.Element("zIndex").Value));
            object content = XamlReader.Load(XmlReader.Create(new StringReader(itemXML.Element("Content").Value)));
            item.Content = content;
            return item;
        }

        /// <summary>
        /// The get bounding rectangle.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <returns>
        /// The <see cref="Rect"/>.
        /// </returns>
        private static Rect GetBoundingRectangle(IEnumerable<DesignerItem> items)
        {
            double x1 = double.MaxValue;
            double y1 = double.MaxValue;
            double x2 = double.MinValue;
            double y2 = double.MinValue;

            foreach (var item in items)
            {
                x1 = Math.Min(Canvas.GetLeft(item), x1);
                y1 = Math.Min(Canvas.GetTop(item), y1);

                x2 = Math.Max(Canvas.GetLeft(item) + item.Width, x2);
                y2 = Math.Max(Canvas.GetTop(item) + item.Height, y2);
            }

            return new Rect(new Point(x1, y1), new Point(x2, y2));
        }

        /// <summary>
        /// The align bottom_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void AlignBottom_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double bottom = Canvas.GetTop(selectedItems.First()) + selectedItems.First().Height;

                foreach (var item in selectedItems)
                {
                    double delta = bottom - (Canvas.GetTop(item) + item.Height);
                    foreach (DesignerItem di in this.SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetTop(di, Canvas.GetTop(di) + delta);
                    }
                }
            }
        }

        /// <summary>
        /// The align horizontal centers_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void AlignHorizontalCenters_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double center = Canvas.GetLeft(selectedItems.First()) + selectedItems.First().Width / 2;

                foreach (var item in selectedItems)
                {
                    double delta = center - (Canvas.GetLeft(item) + item.Width / 2);
                    foreach (DesignerItem di in this.SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetLeft(di, Canvas.GetLeft(di) + delta);
                    }
                }
            }
        }

        /// <summary>
        /// The align left_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void AlignLeft_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double left = Canvas.GetLeft(selectedItems.First());

                foreach (var item in selectedItems)
                {
                    double delta = left - Canvas.GetLeft(item);
                    foreach (DesignerItem di in this.SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetLeft(di, Canvas.GetLeft(di) + delta);
                    }
                }
            }
        }

        /// <summary>
        /// The align right_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void AlignRight_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double right = Canvas.GetLeft(selectedItems.First()) + selectedItems.First().Width;

                foreach (var item in selectedItems)
                {
                    double delta = right - (Canvas.GetLeft(item) + item.Width);
                    foreach (DesignerItem di in this.SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetLeft(di, Canvas.GetLeft(di) + delta);
                    }
                }
            }
        }

        /// <summary>
        /// The align top_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void AlignTop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double top = Canvas.GetTop(selectedItems.First());

                foreach (var item in selectedItems)
                {
                    double delta = top - Canvas.GetTop(item);
                    foreach (DesignerItem di in this.SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetTop(di, Canvas.GetTop(di) + delta);
                    }
                }
            }
        }

        /// <summary>
        /// The align vertical centers_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void AlignVerticalCenters_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double bottom = Canvas.GetTop(selectedItems.First()) + selectedItems.First().Height / 2;

                foreach (var item in selectedItems)
                {
                    double delta = bottom - (Canvas.GetTop(item) + item.Height / 2);
                    foreach (DesignerItem di in this.SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetTop(di, Canvas.GetTop(di) + delta);
                    }
                }
            }
        }

        /// <summary>
        /// The align_ enabled.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Align_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            // var groupedItem = from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
            // where item.ParentID == Guid.Empty
            // select item;

            // e.CanExecute = groupedItem.Count() > 1;
            e.CanExecute = true;
        }

        /// <summary>
        /// The belong to same group.
        /// </summary>
        /// <param name="item1">
        /// The item 1.
        /// </param>
        /// <param name="item2">
        /// The item 2.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool BelongToSameGroup(IGroupable item1, IGroupable item2)
        {
            IGroupable root1 = this.SelectionService.GetGroupRoot(item1);
            IGroupable root2 = this.SelectionService.GetGroupRoot(item2);

            return root1.ID == root2.ID;
        }

        /// <summary>
        /// The bring forward_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BringForward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<UIElement> ordered = (from item in this.SelectionService.CurrentSelection
                                       orderby Panel.GetZIndex(item as UIElement) descending
                                       select item as UIElement).ToList();

            int count = this.Children.Count;

            for (int i = 0; i < ordered.Count; i++)
            {
                int currentIndex = Panel.GetZIndex(ordered[i]);
                int newIndex = Math.Min(count - 1 - i, currentIndex + 1);
                if (currentIndex != newIndex)
                {
                    Panel.SetZIndex(ordered[i], newIndex);
                    IEnumerable<UIElement> it =
                        this.Children.OfType<UIElement>().Where(item => Panel.GetZIndex(item) == newIndex);

                    foreach (var elm in it)
                    {
                        if (elm != ordered[i])
                        {
                            Panel.SetZIndex(elm, currentIndex);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The bring to front_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BringToFront_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<UIElement> selectionSorted = (from item in this.SelectionService.CurrentSelection
                                               orderby Panel.GetZIndex(item as UIElement) ascending
                                               select item as UIElement).ToList();

            List<UIElement> childrenSorted =
                (from UIElement item in this.Children orderby Panel.GetZIndex(item) ascending select item).ToList();

            int i = 0;
            int j = 0;
            foreach (var item in childrenSorted)
            {
                if (selectionSorted.Contains(item))
                {
                    int idx = Panel.GetZIndex(item);
                    Panel.SetZIndex(item, childrenSorted.Count - selectionSorted.Count + j++);
                }
                else
                {
                    Panel.SetZIndex(item, i++);
                }
            }
        }

        /// <summary>
        /// The copy current selection.
        /// </summary>
        private void CopyCurrentSelection()
        {
            IEnumerable<DesignerItem> selectedDesignerItems =
                this.SelectionService.CurrentSelection.OfType<DesignerItem>();

            List<Connection> selectedConnections = this.SelectionService.CurrentSelection.OfType<Connection>().ToList();

            foreach (var connection in from connection in this.Children.OfType<Connection>()
                                       where !selectedConnections.Contains(connection)
                                       let sourceItem =
                                           (from item in selectedDesignerItems
                                            where item.ID == connection.Source.ParentDesignerItem.ID
                                            select item).FirstOrDefault()
                                       let sinkItem =
                                           (from item in selectedDesignerItems
                                            where item.ID == connection.Sink.ParentDesignerItem.ID
                                            select item).FirstOrDefault()
                                       where
                                           sourceItem != null && sinkItem != null
                                           && this.BelongToSameGroup(sourceItem, sinkItem)
                                       select connection)
            {
                selectedConnections.Add(connection);
            }

            XElement designerItemsXML = this.SerializeDesignerItems(selectedDesignerItems);
            XElement connectionsXML = this.SerializeConnections(selectedConnections);

            var root = new XElement("Root");
            root.Add(designerItemsXML);
            root.Add(connectionsXML);

            root.Add(new XAttribute("OffsetX", 10));
            root.Add(new XAttribute("OffsetY", 10));

            Clipboard.Clear();
            Clipboard.SetData(DataFormats.Xaml, root);
        }

        /// <summary>
        /// The copy_ enabled.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Copy_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.SelectionService.CurrentSelection.Count() > 0;
        }

        /// <summary>
        /// The copy_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.CopyCurrentSelection();
        }

        /// <summary>
        /// The cut_ enabled.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Cut_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.SelectionService.CurrentSelection.Count() > 0;
        }

        /// <summary>
        /// The cut_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Cut_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.CopyCurrentSelection();
            this.DeleteCurrentSelection();
        }

        /// <summary>
        /// The delete current selection.
        /// </summary>
        private void DeleteCurrentSelection()
        {
            foreach (var connection in this.SelectionService.CurrentSelection.OfType<Connection>())
            {
                this.Children.Remove(connection);
            }

            foreach (var item in this.SelectionService.CurrentSelection.OfType<DesignerItem>())
            {
                var cd = item.Template.FindName("PART_ConnectorDecorator", item) as Control;

                var connectors = new List<Connector>();
                this.GetConnectors(cd, connectors);

                foreach (var connector in connectors)
                {
                    foreach (var con in connector.Connections)
                    {
                        this.Children.Remove(con);
                    }
                }

                this.Children.Remove(item);
            }

            this.SelectionService.ClearSelection();
            this.UpdateZIndex();
        }

        /// <summary>
        /// The delete_ enabled.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Delete_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.SelectionService.CurrentSelection.Count() > 0;
        }

        /// <summary>
        /// The delete_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DeleteCurrentSelection();
        }

        /// <summary>
        /// The distribute horizontal_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DistributeHorizontal_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                let itemLeft = Canvas.GetLeft(item)
                                orderby itemLeft
                                select item;

            if (selectedItems.Count() > 1)
            {
                double left = double.MaxValue;
                double right = double.MinValue;
                double sumWidth = 0;
                foreach (var item in selectedItems)
                {
                    left = Math.Min(left, Canvas.GetLeft(item));
                    right = Math.Max(right, Canvas.GetLeft(item) + item.Width);
                    sumWidth += item.Width;
                }

                double distance = Math.Max(0, (right - left - sumWidth) / (selectedItems.Count() - 1));
                double offset = Canvas.GetLeft(selectedItems.First());

                foreach (var item in selectedItems)
                {
                    double delta = offset - Canvas.GetLeft(item);
                    foreach (DesignerItem di in this.SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetLeft(di, Canvas.GetLeft(di) + delta);
                    }

                    offset = offset + item.Width + distance;
                }
            }
        }

        /// <summary>
        /// The distribute vertical_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DistributeVertical_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                let itemTop = Canvas.GetTop(item)
                                orderby itemTop
                                select item;

            if (selectedItems.Count() > 1)
            {
                double top = double.MaxValue;
                double bottom = double.MinValue;
                double sumHeight = 0;
                foreach (var item in selectedItems)
                {
                    top = Math.Min(top, Canvas.GetTop(item));
                    bottom = Math.Max(bottom, Canvas.GetTop(item) + item.Height);
                    sumHeight += item.Height;
                }

                double distance = Math.Max(0, (bottom - top - sumHeight) / (selectedItems.Count() - 1));
                double offset = Canvas.GetTop(selectedItems.First());

                foreach (var item in selectedItems)
                {
                    double delta = offset - Canvas.GetTop(item);
                    foreach (DesignerItem di in this.SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetTop(di, Canvas.GetTop(di) + delta);
                    }

                    offset = offset + item.Height + distance;
                }
            }
        }

        /// <summary>
        /// The distribute_ enabled.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Distribute_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            // var groupedItem = from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
            // where item.ParentID == Guid.Empty
            // select item;

            // e.CanExecute = groupedItem.Count() > 1;
            e.CanExecute = true;
        }

        /// <summary>
        /// The get connector.
        /// </summary>
        /// <param name="itemID">
        /// The item id.
        /// </param>
        /// <param name="connectorName">
        /// The connector name.
        /// </param>
        /// <returns>
        /// The <see cref="Connector"/>.
        /// </returns>
        private Connector GetConnector(Guid itemID, string connectorName)
        {
            DesignerItem designerItem =
                (from item in this.Children.OfType<DesignerItem>() where item.ID == itemID select item).FirstOrDefault();

            var connectorDecorator = designerItem.Template.FindName("PART_ConnectorDecorator", designerItem) as Control;
            connectorDecorator.ApplyTemplate();

            return connectorDecorator.Template.FindName(connectorName, connectorDecorator) as Connector;
        }

        /// <summary>
        /// The get connectors.
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        /// <param name="connectors">
        /// The connectors.
        /// </param>
        private void GetConnectors(DependencyObject parent, List<Connector> connectors)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is Connector)
                {
                    connectors.Add(child as Connector);
                }
                else
                {
                    this.GetConnectors(child, connectors);
                }
            }
        }

        /// <summary>
        /// The group_ enabled.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Group_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            int count =
                (from item in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                 where item.ParentID == Guid.Empty
                 select item).Count();

            e.CanExecute = count > 1;
        }

        /// <summary>
        /// The group_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Group_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var items = from item in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                        where item.ParentID == Guid.Empty
                        select item;

            Rect rect = GetBoundingRectangle(items);

            var groupItem = new DesignerItem();
            groupItem.IsGroup = true;
            groupItem.Width = rect.Width;
            groupItem.Height = rect.Height;
            Canvas.SetLeft(groupItem, rect.Left);
            Canvas.SetTop(groupItem, rect.Top);
            var groupCanvas = new Canvas();
            groupItem.Content = groupCanvas;
            Panel.SetZIndex(groupItem, this.Children.Count);
            this.Children.Add(groupItem);

            foreach (var item in items)
            {
                item.ParentID = groupItem.ID;
            }

            this.SelectionService.SelectItem(groupItem);
        }

        /// <summary>
        /// The load serialized data from clip board.
        /// </summary>
        /// <returns>
        /// The <see cref="XElement"/>.
        /// </returns>
        private XElement LoadSerializedDataFromClipBoard()
        {
            if (Clipboard.ContainsData(DataFormats.Xaml))
            {
                var clipboardData = Clipboard.GetData(DataFormats.Xaml) as String;

                if (string.IsNullOrEmpty(clipboardData))
                {
                    return null;
                }

                try
                {
                    return XElement.Load(new StringReader(clipboardData));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.StackTrace, e.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return null;
        }

        /// <summary>
        /// The load serialized data from file.
        /// </summary>
        /// <returns>
        /// The <see cref="XElement"/>.
        /// </returns>
        private XElement LoadSerializedDataFromFile()
        {
            var openFile = new OpenFileDialog();
            openFile.Filter = "Designer Files (*.xml)|*.xml|All Files (*.*)|*.*";

            if (openFile.ShowDialog() == true)
            {
                try
                {
                    return XElement.Load(openFile.FileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.StackTrace, e.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return null;
        }

        /// <summary>
        /// The new_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow window = Helper.GetMainwindow(this);

            var command = window.CommandManager.SelectCommand("NewDiagramCommand");
            command.Execute(window);
        }

        /// <summary>
        /// The open_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            XElement root = this.LoadSerializedDataFromFile();

            if (root == null)
            {
                return;
            }

            this.Children.Clear();
            this.SelectionService.ClearSelection();

            IEnumerable<XElement> itemsXML = root.Elements("DesignerItems").Elements("DesignerItem");
            foreach (var itemXML in itemsXML)
            {
                var id = new Guid(itemXML.Element("ID").Value);
                DesignerItem item = DeserializeDesignerItem(itemXML, id, 0, 0);
                this.Children.Add(item);
                this.SetConnectorDecoratorTemplate(item);
            }

            this.InvalidateVisual();

            IEnumerable<XElement> connectionsXML = root.Elements("Connections").Elements("Connection");
            foreach (var connectionXML in connectionsXML)
            {
                var sourceID = new Guid(connectionXML.Element("SourceID").Value);
                var sinkID = new Guid(connectionXML.Element("SinkID").Value);

                string sourceConnectorName = connectionXML.Element("SourceConnectorName").Value;
                string sinkConnectorName = connectionXML.Element("SinkConnectorName").Value;

                Connector sourceConnector = this.GetConnector(sourceID, sourceConnectorName);
                Connector sinkConnector = this.GetConnector(sinkID, sinkConnectorName);

                var connection = new Connection(sourceConnector, sinkConnector);
                Panel.SetZIndex(connection, int.Parse(connectionXML.Element("zIndex").Value));
                this.Children.Add(connection);
            }
        }

        /// <summary>
        /// The order_ enabled.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Order_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            // e.CanExecute = SelectionService.CurrentSelection.Count() > 0;
            e.CanExecute = true;
        }

        /// <summary>
        /// The paste_ enabled.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Paste_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsData(DataFormats.Xaml);
        }

        /// <summary>
        /// The paste_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            XElement root = this.LoadSerializedDataFromClipBoard();

            if (root == null)
            {
                return;
            }

            // create DesignerItems
            var mappingOldToNewIDs = new Dictionary<Guid, Guid>();
            var newItems = new List<ISelectable>();
            IEnumerable<XElement> itemsXML = root.Elements("DesignerItems").Elements("DesignerItem");

            double offsetX = double.Parse(root.Attribute("OffsetX").Value, CultureInfo.InvariantCulture);
            double offsetY = double.Parse(root.Attribute("OffsetY").Value, CultureInfo.InvariantCulture);

            foreach (var itemXML in itemsXML)
            {
                var oldID = new Guid(itemXML.Element("ID").Value);
                Guid newID = Guid.NewGuid();
                mappingOldToNewIDs.Add(oldID, newID);
                DesignerItem item = DeserializeDesignerItem(itemXML, newID, offsetX, offsetY);
                this.Children.Add(item);
                this.SetConnectorDecoratorTemplate(item);
                newItems.Add(item);
            }

            // update group hierarchy
            this.SelectionService.ClearSelection();
            foreach (DesignerItem el in newItems)
            {
                if (el.ParentID != Guid.Empty)
                {
                    el.ParentID = mappingOldToNewIDs[el.ParentID];
                }
            }

            foreach (DesignerItem item in newItems)
            {
                if (item.ParentID == Guid.Empty)
                {
                    this.SelectionService.AddToSelection(item);
                }
            }

            // create Connections
            IEnumerable<XElement> connectionsXML = root.Elements("Connections").Elements("Connection");
            foreach (var connectionXML in connectionsXML)
            {
                var oldSourceID = new Guid(connectionXML.Element("SourceID").Value);
                var oldSinkID = new Guid(connectionXML.Element("SinkID").Value);

                if (mappingOldToNewIDs.ContainsKey(oldSourceID) && mappingOldToNewIDs.ContainsKey(oldSinkID))
                {
                    Guid newSourceID = mappingOldToNewIDs[oldSourceID];
                    Guid newSinkID = mappingOldToNewIDs[oldSinkID];

                    string sourceConnectorName = connectionXML.Element("SourceConnectorName").Value;
                    string sinkConnectorName = connectionXML.Element("SinkConnectorName").Value;

                    Connector sourceConnector = this.GetConnector(newSourceID, sourceConnectorName);
                    Connector sinkConnector = this.GetConnector(newSinkID, sinkConnectorName);

                    var connection = new Connection(sourceConnector, sinkConnector);
                    Panel.SetZIndex(connection, int.Parse(connectionXML.Element("zIndex").Value));
                    this.Children.Add(connection);

                    this.SelectionService.AddToSelection(connection);
                }
            }

            DiagramCanvas.BringToFront.Execute(null, this);

            // update paste offset
            root.Attribute("OffsetX").Value = (offsetX + 10).ToString();
            root.Attribute("OffsetY").Value = (offsetY + 10).ToString();
            Clipboard.Clear();
            Clipboard.SetData(DataFormats.Xaml, root);
        }

        /// <summary>
        /// The print_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.SelectionService.ClearSelection();

            var printDialog = new PrintDialog();

            if (true == printDialog.ShowDialog())
            {
                printDialog.PrintVisual(this, "WPF Diagram");
            }
        }

        /// <summary>
        /// The save file.
        /// </summary>
        /// <param name="xElement">
        /// The x element.
        /// </param>
        private void SaveFile(XElement xElement)
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = "Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    xElement.Save(saveFile.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// The save_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IEnumerable<DesignerItem> designerItems = this.Children.OfType<DesignerItem>();
            IEnumerable<Connection> connections = this.Children.OfType<Connection>();

            XElement designerItemsXML = this.SerializeDesignerItems(designerItems);
            XElement connectionsXML = this.SerializeConnections(connections);

            var root = new XElement("Root");
            root.Add(designerItemsXML);
            root.Add(connectionsXML);

            this.SaveFile(root);
        }

        /// <summary>
        /// The select all_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SelectAll_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.SelectionService.SelectAll();
        }

        /// <summary>
        /// The send backward_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SendBackward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<UIElement> ordered = (from item in this.SelectionService.CurrentSelection
                                       orderby Panel.GetZIndex(item as UIElement) ascending
                                       select item as UIElement).ToList();

            int count = this.Children.Count;

            for (int i = 0; i < ordered.Count; i++)
            {
                int currentIndex = Panel.GetZIndex(ordered[i]);
                int newIndex = Math.Max(i, currentIndex - 1);
                if (currentIndex != newIndex)
                {
                    Panel.SetZIndex(ordered[i], newIndex);
                    IEnumerable<UIElement> it =
                        this.Children.OfType<UIElement>().Where(item => Panel.GetZIndex(item) == newIndex);

                    foreach (var elm in it)
                    {
                        if (elm != ordered[i])
                        {
                            Panel.SetZIndex(elm, currentIndex);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The send to back_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SendToBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<UIElement> selectionSorted = (from item in this.SelectionService.CurrentSelection
                                               orderby Panel.GetZIndex(item as UIElement) ascending
                                               select item as UIElement).ToList();

            List<UIElement> childrenSorted =
                (from UIElement item in this.Children orderby Panel.GetZIndex(item) ascending select item).ToList();
            int i = 0;
            int j = 0;
            foreach (var item in childrenSorted)
            {
                if (selectionSorted.Contains(item))
                {
                    int idx = Panel.GetZIndex(item);
                    Panel.SetZIndex(item, j++);
                }
                else
                {
                    Panel.SetZIndex(item, selectionSorted.Count + i++);
                }
            }
        }

        /// <summary>
        /// The serialize connections.
        /// </summary>
        /// <param name="connections">
        /// The connections.
        /// </param>
        /// <returns>
        /// The <see cref="XElement"/>.
        /// </returns>
        private XElement SerializeConnections(IEnumerable<Connection> connections)
        {
            var serializedConnections = new XElement(
                "Connections", 
                from connection in connections
                select
                    new XElement(
                    "Connection", 
                    new XElement("SourceID", connection.Source.ParentDesignerItem.ID), 
                    new XElement("SinkID", connection.Sink.ParentDesignerItem.ID), 
                    new XElement("SourceConnectorName", connection.Source.Name), 
                    new XElement("SinkConnectorName", connection.Sink.Name), 
                    new XElement("SourceArrowSymbol", connection.SourceArrowSymbol), 
                    new XElement("SinkArrowSymbol", connection.SinkArrowSymbol), 
                    new XElement("zIndex", Panel.GetZIndex(connection))));

            return serializedConnections;
        }

        /// <summary>
        /// The serialize designer items.
        /// </summary>
        /// <param name="designerItems">
        /// The designer items.
        /// </param>
        /// <returns>
        /// The <see cref="XElement"/>.
        /// </returns>
        private XElement SerializeDesignerItems(IEnumerable<DesignerItem> designerItems)
        {
            var serializedItems = new XElement(
                "DesignerItems", 
                from item in designerItems
                let contentXaml = XamlWriter.Save(item.Content)
                select
                    new XElement(
                    "DesignerItem", 
                    new XElement("Left", Canvas.GetLeft(item)), 
                    new XElement("Top", Canvas.GetTop(item)), 
                    new XElement("Width", item.Width), 
                    new XElement("Height", item.Height), 
                    new XElement("ID", item.ID), 
                    new XElement("zIndex", Panel.GetZIndex(item)), 
                    new XElement("IsGroup", item.IsGroup), 
                    new XElement("ParentID", item.ParentID), 
                    new XElement("Content", contentXaml)));

            return serializedItems;
        }

        /// <summary>
        /// The ungroup_ enabled.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Ungroup_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            var groupedItem = from item in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                              where item.ParentID != Guid.Empty
                              select item;

            e.CanExecute = groupedItem.Count() > 0;
        }

        /// <summary>
        /// The ungroup_ executed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Ungroup_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var groups = (from item in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                          where item.IsGroup && item.ParentID == Guid.Empty
                          select item).ToArray();

            foreach (var groupRoot in groups)
            {
                var children = from child in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                               where child.ParentID == groupRoot.ID
                               select child;

                foreach (var child in children)
                {
                    child.ParentID = Guid.Empty;
                }

                this.SelectionService.RemoveFromSelection(groupRoot);
                this.Children.Remove(groupRoot);
                this.UpdateZIndex();
            }
        }

        /// <summary>
        /// The update z index.
        /// </summary>
        private void UpdateZIndex()
        {
            List<UIElement> ordered =
                (from UIElement item in this.Children orderby Panel.GetZIndex(item) select item).ToList();

            for (int i = 0; i < ordered.Count; i++)
            {
                Panel.SetZIndex(ordered[i], i);
            }
        }

        #endregion
    }
}