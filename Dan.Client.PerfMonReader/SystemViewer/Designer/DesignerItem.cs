// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DesignerItem.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The designer item.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Designer
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    using SystemViewer.Controls;
    using SystemViewer.Designer.Controls;
    using SystemViewer.gridClasses;

    using Dan.monitor.Common;

    // These attributes identify the types of the named parts that are used for templating
    /// <summary>
    /// The designer item.
    /// </summary>
    [TemplatePart(Name = "PART_DragThumb", Type = typeof(DragThumb))]
    [TemplatePart(Name = "PART_ResizeDecorator", Type = typeof(Control))]
    [TemplatePart(Name = "PART_ConnectorDecorator", Type = typeof(Control))]
    [TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
    public class DesignerItem : ContentControl, ISelectable, IGroupable
    {
        #region Static Fields

        /// <summary>
        /// The connector decorator template property.
        /// </summary>
        public static readonly DependencyProperty ConnectorDecoratorTemplateProperty =
            DependencyProperty.RegisterAttached(
                "ConnectorDecoratorTemplate", 
                typeof(ControlTemplate), 
                typeof(DesignerItem));

        /// <summary>
        /// The drag thumb template property.
        /// </summary>
        public static readonly DependencyProperty DragThumbTemplateProperty =
            DependencyProperty.RegisterAttached("DragThumbTemplate", typeof(ControlTemplate), typeof(DesignerItem));

        /// <summary>
        /// The is drag connection over property.
        /// </summary>
        public static readonly DependencyProperty IsDragConnectionOverProperty =
            DependencyProperty.Register(
                "IsDragConnectionOver", 
                typeof(bool), 
                typeof(DesignerItem), 
                new FrameworkPropertyMetadata(false));

        /// <summary>
        /// The is group property.
        /// </summary>
        public static readonly DependencyProperty IsGroupProperty = DependencyProperty.Register(
            "IsGroup", 
            typeof(bool), 
            typeof(DesignerItem));

        /// <summary>
        /// The is selected property.
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected", 
            typeof(bool), 
            typeof(DesignerItem), 
            new FrameworkPropertyMetadata(false));

        /// <summary>
        /// The parent id property.
        /// </summary>
        public static readonly DependencyProperty ParentIDProperty = DependencyProperty.Register(
            "ParentID", 
            typeof(Guid), 
            typeof(DesignerItem));

        #endregion

        #region Fields

        /// <summary>
        /// The id.
        /// </summary>
        private Guid id;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="DesignerItem"/> class.
        /// </summary>
        static DesignerItem()
        {
            // set the key to reference the style for this control
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DesignerItem), 
                new FrameworkPropertyMetadata(typeof(DesignerItem)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignerItem"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public DesignerItem(Guid id)
        {
            this.id = id;
            this.Loaded += this.DesignerItem_Loaded;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignerItem"/> class.
        /// </summary>
        public DesignerItem()
            : this(Guid.NewGuid())
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid ID
        {
            get
            {
                return this.id;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is drag connection over.
        /// </summary>
        public bool IsDragConnectionOver
        {
            get
            {
                return (bool)this.GetValue(IsDragConnectionOverProperty);
            }

            set
            {
                this.SetValue(IsDragConnectionOverProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is group.
        /// </summary>
        public bool IsGroup
        {
            get
            {
                return (bool)this.GetValue(IsGroupProperty);
            }

            set
            {
                this.SetValue(IsGroupProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is selected.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return (bool)this.GetValue(IsSelectedProperty);
            }

            set
            {
                this.SetValue(IsSelectedProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        public Guid ParentID
        {
            get
            {
                return (Guid)this.GetValue(ParentIDProperty);
            }

            set
            {
                this.SetValue(ParentIDProperty, value);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get connector decorator template.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="ControlTemplate"/>.
        /// </returns>
        public static ControlTemplate GetConnectorDecoratorTemplate(UIElement element)
        {
            return (ControlTemplate)element.GetValue(ConnectorDecoratorTemplateProperty);
        }

        /// <summary>
        /// The get drag thumb template.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="ControlTemplate"/>.
        /// </returns>
        public static ControlTemplate GetDragThumbTemplate(UIElement element)
        {
            return (ControlTemplate)element.GetValue(DragThumbTemplateProperty);
        }

        /// <summary>
        /// The set connector decorator template.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void SetConnectorDecoratorTemplate(UIElement element, ControlTemplate value)
        {
            element.SetValue(ConnectorDecoratorTemplateProperty, value);
        }

        /// <summary>
        /// The set drag thumb template.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void SetDragThumbTemplate(UIElement element, ControlTemplate value)
        {
            element.SetValue(DragThumbTemplateProperty, value);
        }

        #endregion

        // while drag connection procedure is ongoing and the mouse moves over 
        // this item this value is true; if true the ConnectorDecorator is triggered
        // to be visible, see template
        #region Methods

        /// <summary>
        /// The on preview mouse down.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            var designer = VisualTreeHelper.GetParent(this) as DiagramCanvas;

            // update selection
            if (designer != null)
            {
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                {
                    if (this.IsSelected)
                    {
                        designer.SelectionService.RemoveFromSelection(this);
                    }
                    else
                    {
                        designer.SelectionService.AddToSelection(this);
                    }
                }
                else if (!this.IsSelected)
                {
                    designer.SelectionService.SelectItem(this);
                }

                this.Focus();
            }

            e.Handled = false;
            PropertiesController.Pgrid.AutoGenerateProperties = true;
            PropertiesController.Pgrid.SelectedObject = new DiagramControlProperties((IMonitorControl)this.Content);
        }

        /// <summary>
        /// The designer item_ loaded.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DesignerItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (base.Template != null)
            {
                var contentPresenter = this.Template.FindName("PART_ContentPresenter", this) as ContentPresenter;
                if (contentPresenter != null)
                {
                    var contentVisual = VisualTreeHelper.GetChild(contentPresenter, 0) as UIElement;
                    if (contentVisual != null)
                    {
                        var thumb = this.Template.FindName("PART_DragThumb", this) as DragThumb;
                        if (thumb != null)
                        {
                            var template = DesignerItem.GetDragThumbTemplate(contentVisual);
                            if (template != null)
                            {
                                thumb.Template = template;
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}