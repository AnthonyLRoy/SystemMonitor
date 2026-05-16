using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;
using System.Windows;

namespace SystemViewer.Controls
{
    public interface IMonitorControl
    {
        string Name { get; set; }
        int UpdateFrequency { get; set; }
        float Xpos { get; set; }
        float Ypos { get; set; }
        float Width { get; set; }
        float Height { get; set; }
        float Warn { get; set; }
        float Alert { get; set; }
        float Ok { get; set; }
        Color WarnColor { get; set; }
        Color AlertColor { get; set; }
        Color OkColor { get; set; }
        bool EmailOnAlert { get; set; }
        string EmailReceipient { get; set; }
        float MonitorValue { get; set; }
    }
}
