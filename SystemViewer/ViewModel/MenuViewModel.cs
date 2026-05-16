// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuViewModel.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The menu view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.ViewModel
{
    using System;
    using System.Collections.ObjectModel;

    using SystemViewer.Models.Menu;

    /// <summary>
    /// The menu view model.
    /// </summary>
    public class MenuViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// The _menu items.
        /// </summary>
        private ObservableCollection<FileMenu> _menuItems;

        /// <summary>
        /// The _selected menu item.
        /// </summary>
        private string _selectedMenuItem;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewModel"/> class.
        /// </summary>
        public MenuViewModel()
        {
            this.FillMenuItems();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The menu selected.
        /// </summary>
        public event EventHandler<FileMenuEventArgs> MenuSelected;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the menu items.
        /// </summary>
        public ObservableCollection<FileMenu> MenuItems
        {
            get
            {
                return this._menuItems = this._menuItems ?? new ObservableCollection<FileMenu>();
            }
        }

        /// <summary>
        /// Gets or sets the selected menu item.
        /// </summary>
        public string SelectedMenuItem
        {
            get
            {
                return this._selectedMenuItem;
            }

            set
            {
                this._selectedMenuItem = value;
                this.OnPropertyChanged("SelectedMenuItem");
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The menu item file menu click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public virtual void MenuItemFileMenuClick(object sender, FileMenuEventArgs args)
        {
            // Force the main form to handle the Event
            this.MenuSelected(sender, args);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The fill menu items.
        /// </summary>
        private void FillMenuItems()
        {
            var file = new FileMenu { Header = "_File", CommandParameter = "file" };
            this.MenuItems.Add(file);

            file.Items.Add(new FileMenu { Header = "_New", CommandParameter = "NewDiagramCommand" });
            file.Items.Add(new FileMenu { Header = "_Open ", CommandParameter = "OpenDiagramCommand" });
            file.Items.Add(new FileMenu { Header = "_Save", CommandParameter = "SaveDiagramCommand" });
            file.Items.Add(new FileMenu { Header = "Exit", CommandParameter = "exitApplicationCommand" });

            var view = new FileMenu { Header = "_View", CommandParameter = "View" };
            this.MenuItems.Add(view);
            view.Items.Add(new FileMenu { Header = "_ToolBox", CommandParameter = "LoadToolBoxCommand" });
            view.Items.Add(new FileMenu { Header = "_Properties", CommandParameter = "LoadPropertiesWindowCommand" });

            var help = new FileMenu { Header = "Help", CommandParameter = "help" };
            this.MenuItems.Add(help);

            help.Items.Add(new FileMenu { Header = "About us", CommandParameter = "about" });

            foreach (var menuItem in this.MenuItems)
            {
                this.RegisterMenuItemsEventHandler(menuItem);
            }
        }

        /// <summary>
        /// The register menu items event handler.
        /// </summary>
        /// <param name="root">
        /// The root.
        /// </param>
        private void RegisterMenuItemsEventHandler(FileMenu root)
        {
            foreach (var item in root.Items)
            {
                item.FileMenuClick += this.MenuItemFileMenuClick;
                if (item.HasChildren)
                {
                    this.RegisterMenuItemsEventHandler(item);
                }
            }
        }

        #endregion
    }
}