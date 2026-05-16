using System.Windows;
using SystemViewer.Models.Menu;
using SystemViewer.ViewModel;

namespace SystemViewer.Forms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private LayoutManager _layoutmanager;
        private Commands.CommandManager _commandManager;
        public MainWindow()
        {
            InitializeComponent();
            InitApplication();

        }

        private void InitApplication()
        {
            BuildDefaultMenu();
            _commandManager = new Commands.CommandManager();
            _layoutmanager = new LayoutManager(dockingmanager);
            RegisterEvents();
            _layoutmanager.AddNewDiagram("Feckitworks");
        }
        
        public LayoutManager Layoutmanager
        {
            get { return _layoutmanager; }
            set { _layoutmanager = value; }
        }

        private void BuildDefaultMenu()
        {
            var vm = new ViewModel.MenuViewModel();
            DataContext = vm;
            vm.MenuSelected += MenuItemSelected;
        }

        void MainWindow_MenuSelected(object sender, FileMenuEventArgs e)
        {
            _commandManager.SelectCommand(e.CommandName);
        }

        void MenuItemSelected(object sender, FileMenuEventArgs e)
        {
            var command = _commandManager.SelectCommand(e.CommandName);
            command.Execute(this);
        }

        private void RegisterEvents()
        {
            new MenuViewModel().MenuSelected += MainWindow_MenuSelected;
        }
    }
}
