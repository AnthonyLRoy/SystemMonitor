using System;
using System.Collections.ObjectModel;
using SystemViewer.Models.Menu;

namespace SystemViewer.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private ObservableCollection<FileMenu> _menuItems;
        private string _selectedMenuItem;

         public event EventHandler<FileMenuEventArgs> MenuSelected;
        
        public MenuViewModel()
        {
            FillMenuItems();
        }

        public ObservableCollection<FileMenu> MenuItems
        {
            get
            {
                return (_menuItems = _menuItems ??new ObservableCollection<FileMenu>());
            }
        }

        public string SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                _selectedMenuItem = value;
                OnPropertyChanged("SelectedMenuItem");
            }
        }

        private void FillMenuItems()
        {
           
            var file = new FileMenu { Header = "_File", CommandParameter = "file" };
            MenuItems.Add(file);

            file.Items.Add(new FileMenu { Header = "_New", CommandParameter = "NewDiagramCommand" });
            file.Items.Add(new FileMenu { Header = "_Open ", CommandParameter = "OpenDiagramCommand" });
            file.Items.Add(new FileMenu { Header = "_Save", CommandParameter = "SaveDiagramCommand" });
            file.Items.Add(new FileMenu { Header = "Exit", CommandParameter = "exitApplicationCommand" });

            var view = new FileMenu { Header = "_View", CommandParameter = "View" };
            MenuItems.Add(view);
            view.Items.Add(new FileMenu { Header = "_ToolBox", CommandParameter = "LoadToolBoxCommand" });
            view.Items.Add(new FileMenu { Header = "_Properties", CommandParameter = "LoadPropertiesWindowCommand" });

            var help = new FileMenu { Header = "Help", CommandParameter = "help" };
            MenuItems.Add(help);

            help.Items.Add(new FileMenu { Header = "About us", CommandParameter = "about" });

            foreach (var menuItem in MenuItems)
            {
                RegisterMenuItemsEventHandler(menuItem);
            }
        }

        private void RegisterMenuItemsEventHandler(FileMenu root)
        {
            foreach (var item in root.Items)
            {
                item.FileMenuClick += MenuItemFileMenuClick;
                if (item.HasChildren)
                {
                    RegisterMenuItemsEventHandler(item);
                }
            }
        }

        public virtual void MenuItemFileMenuClick(object sender, FileMenuEventArgs args)
        {

            //Force the main form to handle the Event
            MenuSelected(sender, args);

        }
    }
}
