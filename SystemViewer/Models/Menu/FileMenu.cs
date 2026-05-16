// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileMenu.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The file menu.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Models.Menu
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using SystemViewer.Infrastructure.Commands;

    /// <summary>
    /// The file menu.
    /// </summary>
    public class FileMenu : ModelBase
    {
        #region Fields

        /// <summary>
        /// The _command.
        /// </summary>
        private ICommand _command;

        /// <summary>
        /// The _is enabled.
        /// </summary>
        private bool _isEnabled = true;

        /// <summary>
        /// The _items.
        /// </summary>
        private ObservableCollection<FileMenu> _items;

        #endregion

        #region Public Events

        /// <summary>
        /// The file menu click.
        /// </summary>
        public event FileMenuHandler FileMenuClick;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the command.
        /// </summary>
        public ICommand Command
        {
            get
            {
                return
                    this._command =
                     this._command ?? new DelegateCommand<string>(this.OnMenuItemClick, x => this.IsEnabled);
            }
        }

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        public string CommandParameter { get; set; }

        /// <summary>
        /// Gets a value indicating whether has children.
        /// </summary>
        public bool HasChildren
        {
            get
            {
                return this.Items.Count > 0;
            }
        }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return this._isEnabled;
            }

            set
            {
                this._isEnabled = value;
                this.OnPropertyChanged("IsEnabled");
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        public ObservableCollection<FileMenu> Items
        {
            get
            {
                return this._items = this._items ?? new ObservableCollection<FileMenu>();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The on file menu click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public virtual void OnFileMenuClick(object sender, FileMenuEventArgs args)
        {
            if (this.FileMenuClick != null)
            {
                this.FileMenuClick(sender, args);
            }
        }

        /// <summary>
        /// The on menu item click.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public virtual void OnMenuItemClick(string parameter)
        {
            this.OnFileMenuClick(this, new FileMenuEventArgs { CommandName = parameter });
        }

        #endregion
    }
}