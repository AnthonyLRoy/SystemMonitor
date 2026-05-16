using System.Collections.Generic;
using System.ComponentModel;
using Dan.monitor.Common;

namespace SystemViewer.Models.ToolBox
{
    public class DataControlItemProperties:IMonitorControl
    {
        private IMonitorControl control;

        public DataControlItemProperties(IMonitorControl control)
        {
            // TODO: Complete member initialization
            this.control = control;
            EmailReceipient = control.EmailReceipient;

        }
        [Category("Monitor details")]
        [DisplayName(@"Name of the control")]
        [DescriptionAttribute("The Control name that has been defined for this form")]
        public string Name { get; set; }

        [Category("Monitor details")]
        [DisplayName(@"update Time")]
        [DescriptionAttribute("The number of seconds before each update")]
        public int UpdateFrequency{ get; set; }


        [Category("location")]
        [DisplayName(@"X position")]
        [DescriptionAttribute("x position on the form")]
        public float Xpos { get; set; }

        [Category("location")]
        [DisplayName(@"Y position")]
        [DescriptionAttribute("Y position on the form")]
        public float Ypos{ get; set; }

        [Category("location")]
        [DisplayName(@"Width")]
        [DescriptionAttribute("The Control name that has been defined for this form")]
        public float Width  { get; set; }
       
        [Category("location")]
        [DisplayName(@"Height")]
        [DescriptionAttribute("The Control name that has been defined for this form")]
        public float Height  { get; set; }


        [Category("settings")]
        [DisplayName(@"Warn")]
        [DescriptionAttribute("The threshold limit for warning to become active")]
        public float Warn  { get; set; }


        [Category("Settings")]
        [DisplayName(@"Alert")]
        [DescriptionAttribute("The threshold limit for alrert to become active")]
        public float Alert  { get; set; }


        [Category("Settings")]
        [DisplayName(@"OK")]
        [DescriptionAttribute("The threshold limit below which within range is ok")]
        public float Ok  { get; set; }


        [Category("Settings")]
        [DisplayName(@"Warning color")]
        [Editor(typeof(System.Drawing.Color),typeof(System.Drawing.Color))]
        [DescriptionAttribute("TThe Color change for a warning")]
        public System.Drawing.Color WarnColor  { get; set; }

        [Category("Settings")]
        [DisplayName(@"Alert color")]
        [DescriptionAttribute("The Color for the component being in an alert state")]
 
        public System.Drawing.Color AlertColor  { get; set; }

        [Category("Settings")]
        [DisplayName(@"OK color")]
        [DescriptionAttribute("The Color for the component being within range")]
        public System.Drawing.Color OkColor  { get; set; }



        [Category("Settings")]
        [DisplayName(@"Email on Alert")]
        [DescriptionAttribute("Email on alert")]
        public bool EmailOnAlert  { get; set; }


        [Category("Settings")]
        [DisplayName(@"Email")]
        [DescriptionAttribute("Destination email addresses for alert")]
        public List<string> EmailReceipient
        {
            get { return control.EmailReceipient; }
            set { control.EmailReceipient = value; }
        }


        [Category("Monitor details")]
        [DisplayName(@"The Current Value")]
        [DescriptionAttribute("The current Read value ")]
        public float MonitorValue  { get; set; }


        [Category("Monitored Events")]
        [DisplayName(@"The Event")]
        [DescriptionAttribute("The current List of events that this control monitors")]
        public string MonitoringEvent  { get; set; }

    }
}
