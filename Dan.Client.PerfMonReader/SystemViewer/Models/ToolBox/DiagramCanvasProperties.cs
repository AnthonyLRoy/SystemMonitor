using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using SystemViewer.Controls;
using Castle.DynamicProxy.Contributors;
using Dan.Common;
using Dan.monitor.Common;


namespace SystemViewer.Models.ToolBox
{
    public class DiagramCanvasProperties
    {
        private readonly DiagramCanvas _diagram;

        public DiagramCanvasProperties(DiagramCanvas diagram)
        {
            _diagram = diagram;
            FillProperties();
        }

        private void FillProperties()
        {
            Name = _diagram.Name;
            BackGroundColor = _diagram.Background;
            Status = _diagram.DiagramStatus;

        }
        
        public string Name { get; set; }
        public DiagramStatus  Status {get;set;}

        public System.Windows.Media.Brush BackGroundColor { get; set; }


    }
}
