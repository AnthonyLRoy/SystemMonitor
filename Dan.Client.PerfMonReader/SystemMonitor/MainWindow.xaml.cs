using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SystemMonitor.Menus;

namespace SystemMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitApplication();
        }

        private void InitApplication()
        {
            var menuManager = new MainFormMenuManager(MainMenu);
            //layoutmanager = new LayoutManager(dockingmanager);
            //_controlManager = new ControlManager(new WPFCustomControlsLib.WPFControlLibrary(), true);
            //layoutmanager.AddNewDiagram("Feckitworks");
        }
    }
}
