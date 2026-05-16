// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectionService.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The selection service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SystemViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SystemViewer.Controls;

    /// <summary>
    /// The selection service.
    /// </summary>
    internal class SelectionService
    {
        #region Fields

        /// <summary>
        /// The current selection.
        /// </summary>
        private List<ISelectable> currentSelection;

        /// <summary>
        /// The diagram canvas.
        /// </summary>
        private DiagramCanvas diagramCanvas;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionService"/> class.
        /// </summary>
        /// <param name="canvas">
        /// The canvas.
        /// </param>
        public SelectionService(DiagramCanvas canvas)
        {
            this.diagramCanvas = canvas;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current selection.
        /// </summary>
        internal List<ISelectable> CurrentSelection
        {
            get
            {
                if (this.currentSelection == null)
                {
                    this.currentSelection = new List<ISelectable>();
                }

                return this.currentSelection;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add to selection.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        internal void AddToSelection(ISelectable item)
        {
            if (item is IGroupable)
            {
                List<IGroupable> groupItems = this.GetGroupMembers(item as IGroupable);

                foreach (ISelectable groupItem in groupItems)
                {
                    groupItem.IsSelected = true;
                    this.CurrentSelection.Add(groupItem);
                }
            }
            else
            {
                item.IsSelected = true;
                this.CurrentSelection.Add(item);
            }
        }

        /// <summary>
        /// The clear selection.
        /// </summary>
        internal void ClearSelection()
        {
            this.CurrentSelection.ForEach(item => item.IsSelected = false);
            this.CurrentSelection.Clear();
        }

        /// <summary>
        /// The get group members.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        internal List<IGroupable> GetGroupMembers(IGroupable item)
        {
            IEnumerable<IGroupable> list = this.diagramCanvas.Children.OfType<IGroupable>();
            IGroupable rootItem = this.GetRoot(list, item);
            return this.GetGroupMembers(list, rootItem);
        }

        /// <summary>
        /// The get group root.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="IGroupable"/>.
        /// </returns>
        internal IGroupable GetGroupRoot(IGroupable item)
        {
            IEnumerable<IGroupable> list = this.diagramCanvas.Children.OfType<IGroupable>();
            return this.GetRoot(list, item);
        }

        /// <summary>
        /// The remove from selection.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        internal void RemoveFromSelection(ISelectable item)
        {
            if (item is IGroupable)
            {
                List<IGroupable> groupItems = this.GetGroupMembers(item as IGroupable);

                foreach (ISelectable groupItem in groupItems)
                {
                    groupItem.IsSelected = false;
                    this.CurrentSelection.Remove(groupItem);
                }
            }
            else
            {
                item.IsSelected = false;
                this.CurrentSelection.Remove(item);
            }
        }

        /// <summary>
        /// The select all.
        /// </summary>
        internal void SelectAll()
        {
            this.ClearSelection();
            this.CurrentSelection.AddRange(this.diagramCanvas.Children.OfType<ISelectable>());
            this.CurrentSelection.ForEach(item => item.IsSelected = true);
        }

        /// <summary>
        /// The select item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        internal void SelectItem(ISelectable item)
        {
            this.ClearSelection();
            this.AddToSelection(item);
        }

        /// <summary>
        /// The get group members.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="parent">
        /// The parent.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private List<IGroupable> GetGroupMembers(IEnumerable<IGroupable> list, IGroupable parent)
        {
            var groupMembers = new List<IGroupable>();
            groupMembers.Add(parent);

            var children = list.Where(node => node.ParentID == parent.ID);

            foreach (var child in children)
            {
                groupMembers.AddRange(this.GetGroupMembers(list, child));
            }

            return groupMembers;
        }

        /// <summary>
        /// The get root.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// The <see cref="IGroupable"/>.
        /// </returns>
        private IGroupable GetRoot(IEnumerable<IGroupable> list, IGroupable node)
        {
            if (node == null || node.ParentID == Guid.Empty)
            {
                return node;
            }

            foreach (var item in list)
            {
                if (item.ID == node.ParentID)
                {
                    return this.GetRoot(list, item);
                }
            }

            return null;
        }

        #endregion
    }
}