// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Forms
{
    using System;
    using System.Windows;

    using SystemViewer.Commands;
    using SystemViewer.Config;
    using SystemViewer.Helpers.Libraries;
    using SystemViewer.Managers;
    using SystemViewer.Models.Menu;
    using SystemViewer.ViewModel;

    using Dan.Common.Client;
    using Dan.monitor.Common;

    using SimpleConfigSections;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        /// <summary>
        /// The _command manager.
        /// </summary>
        private CommandManager _commandManager;

        /// <summary>
        /// The _layoutmanager.
        /// </summary>
        private LayoutManager _layoutmanager;



        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.InitApplication();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the command manager.
        /// </summary>
        public CommandManager CommandManager
        {
            get
            {
                return this._commandManager;
            }

            set
            {
                this._commandManager = value;
            }
        }



        /// <summary>
        /// Gets or sets the layoutmanager.
        /// </summary>
        public LayoutManager Layoutmanager
        {
            get
            {
                return this._layoutmanager;
            }

            set
            {
                this._layoutmanager = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The build default menu.
        /// </summary>
        private void BuildDefaultMenu()
        {
            var vm = new MenuViewModel();
            this.DataContext = vm;
            vm.MenuSelected += this.MenuItemSelected;
        }

        /// <summary>
        /// The init application.
        /// </summary>
        private void InitApplication()
        {
            this.LoadExternalControlAssemblies();
            this.LoadExternalReaderLibraries();
            this.BuildDefaultMenu();
            this._commandManager = new Commands.CommandManager();
            this._layoutmanager = new LayoutManager(this.dockingmanager);
            this.RegisterWebReaders();
            this.RegisterMenuEvents();
            this.RegisterTimedCollectors();
        }

        /// <summary>
        /// Just make sure that the Assemblies have been loaded into the domain before we attempt to access them
        /// </summary>
        private void LoadExternalControlAssemblies()
        {
            new ControlLibraries(Configuration.Get<IExternalReaderLibrary>()).LoadLibraries();
        }

        /// <summary>
        /// The load external reader libraries.
        /// </summary>
        private void LoadExternalReaderLibraries()
        {
            new ReaderLibrarys(Configuration.Get<IMetricReaderLibrary>()).LoadLibraries();
        }

        /// <summary>
        /// The main window_ menu selected.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MainWindow_MenuSelected(object sender, FileMenuEventArgs e)
        {
            this._commandManager.SelectCommand(e.CommandName);
        }

        /// <summary>
        /// The menu item selected.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MenuItemSelected(object sender, FileMenuEventArgs e)
        {
            var command = this._commandManager.SelectCommand(e.CommandName);
            command.Execute(this);
        }

        /// <summary>
        /// The register menu events.
        /// </summary>
        private void RegisterMenuEvents()
        {
            new MenuViewModel().MenuSelected += this.MainWindow_MenuSelected;
        }

        /// <summary>
        /// The register timed collectors.
        /// </summary>
        private void RegisterTimedCollectors()
        {
           
  
        }

        /// <summary>
        /// The register web readers.
        /// </summary>
        private void RegisterWebReaders()
        {
            WebReaders.Init();
        }

        #endregion
    }
}