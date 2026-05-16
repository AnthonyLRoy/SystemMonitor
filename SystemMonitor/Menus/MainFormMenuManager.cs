        using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
namespace SystemMonitor.Menus


{
    public class MainFormMenuManager
    {
        private System.Windows.Controls.Menu _mainMenu;


        public MainFormMenuManager()
        {
            
        }
        public MainFormMenuManager(System.Windows.Controls.Menu mainMenu)
        {
            _mainMenu = mainMenu;
            InitDefaultMenuItems();
        }

        public void InitDefaultMenuItems()
        {

            var menu = ReadDefaultMenu();
            MenuItem menuItem = new MenuItem();
            menuItem.Header = "_File";

            _mainMenu.Items.Add(menuItem);
        }

        private object ReadDefaultMenu()
        {
            string filePath = @"files\DefaultMenu.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(filePath);

            return null;
        }

    }
}
