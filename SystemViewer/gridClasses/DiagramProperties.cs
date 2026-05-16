// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagramProperties.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The diagram properties.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.gridClasses
{
    using System.ComponentModel;
    using System.Windows.Media;

    using SystemViewer.Controls;

    using Dan.monitor.Common;

    using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

    /// <summary>
    /// The diagram properties.
    /// </summary>
    public class DiagramProperties
    {
        #region Fields

        /// <summary>
        /// The _diagram.
        /// </summary>
        private readonly DiagramCanvas _diagram;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagramProperties"/> class.
        /// </summary>
        /// <param name="diagram">
        /// The diagram.
        /// </param>
        public DiagramProperties(DiagramCanvas diagram)
        {
            this._diagram = diagram;
            this.FillProperties();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the back ground color.
        /// </summary>
        [Editor(typeof(ColorEditor), typeof(ColorEditor))]
        public Color BackGroundColor
        {
            get
            {
                var cc = new ColorConverter();

                return (System.Windows.Media.Color)ColorConverter.ConvertFromString(this._diagram.Background.ToString());
            }

            set
            {
                var conv = new BrushConverter();

                this._diagram.Background = (Brush)conv.ConvertFromString(value.ToString());
            }
        }
        
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public DiagramStatus Status 
        {
            get
            {
                return this._diagram.DiagramStatus;
            }

            set
            {

                this._diagram.DiagramStatus = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The fill properties.
        /// </summary>
        private void FillProperties()
        {
            this.Name = this._diagram.Name;

            this.Status = this._diagram.DiagramStatus;
        }

        #endregion
    }
}