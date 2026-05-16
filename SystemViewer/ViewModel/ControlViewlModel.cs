// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlViewlModel.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The control viewl model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Windows;

    using SystemViewer.Models.ToolBox;

    using Dan.monitor.Common;

    /// <summary>
    /// The control view model.
    /// </summary>
    public class ControlViewlModel : INotifyPropertyChanged
    {
        #region Fields


        private const string IconImageUri = "pack://application:,,,/{0};component/{1}";
        /// <summary>
        /// The _diagram controls.
        /// </summary>
        private readonly IEnumerable<DiagramToolBoxItem> _diagramControls = new List<DiagramToolBoxItem>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlViewlModel"/> class.
        /// </summary>
        public ControlViewlModel()
        {
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                this._diagramControls =
                    AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(
                            x =>
                            x.GetTypes()
                                .Where(
                                    t =>
                                    t.GetCustomAttribute<MonitorControlAttribute>() != null
                                    && t.GetCustomAttribute<MonitorControlAttribute>().IsInUse))
                        .ToList()
                        .Select(
                            x =>
                            new DiagramToolBoxItem
                                {
                                    Library = x.Assembly,
                                    Fullname = x.FullName, 
                                    Name = x.Name, 
                                    Description = (string)x.GetMethod("ControlDescription").Invoke(null, null),
                                    IconImage = new Uri(string.Format(IconImageUri,x.Assembly, x.GetMethod("icon").Invoke(null, null)), UriKind.Absolute),
                                    IsActive = true
                                });
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the diagram controls.
        /// </summary>
        public IEnumerable<DiagramToolBoxItem> DiagramControls
        {
            get
            {
                return this._diagramControls;
            }
        }

        #endregion
    }
}