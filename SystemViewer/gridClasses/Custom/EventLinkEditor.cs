// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventLinkEditor.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The monitor attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.gridClasses.Custom
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    using SystemViewer.Forms.dialogs;

    using Dan.monitor.Common;

    using Xceed.Wpf.Toolkit.PropertyGrid;
    using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

    /// <summary>
    /// The monitor attribute.
    /// </summary>
    public class MonitorAttribute : ITypeEditor
    {
        #region Fields

        /// <summary>
        /// The _textbox.
        /// </summary>
        private TextBox _textbox;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The resolve editor.
        /// </summary>
        /// <param name="propertyItem">
        /// The property item.
        /// </param>
        /// <returns>
        /// The <see cref="FrameworkElement"/>.
        /// </returns>
        public FrameworkElement ResolveEditor(PropertyItem propertyItem)
        {
            var dp = new DockPanel();
            dp.LastChildFill = true;
            var bt = new Button();
            bt.Content = "...";
            bt.Click += this.bt_Click;
            DockPanel.SetDock(bt, Dock.Right);
            dp.Children.Add(bt);
            this._textbox = new TextBox { Text = "xyz" };
            dp.Children.Add(this._textbox);

            // create the binding from the bound property item to the editor
            var binding = new Binding("Value")
                              {
                                  Source = propertyItem, 
                                  ValidatesOnExceptions = true, 
                                  ValidatesOnDataErrors = true, 
                                  Mode =
                                      propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay
                              };
                
                // bind to the Value property of the PropertyItem
            BindingOperations.SetBinding(this._textbox, TextBox.TextProperty, binding);
            return dp;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The bt_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void bt_Click(object sender, RoutedEventArgs e)
        {
            var lnk = new frmEventLink();
            lnk.ShowDialog();
            if (lnk.DialogResult == true)
            {
                this._textbox.Text = ((GroupName)lnk.CboGroups.SelectedItem).Groupname + ":"
                                     + lnk.LstEventList.SelectedItem;
            }
        }

        #endregion
    }
}