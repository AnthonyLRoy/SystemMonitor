// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenDiagramCommand.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The open diagram command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Infrastructure.Commands
{
    using System;
    using System.IO;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;

    using SystemViewer.Controls;
    using SystemViewer.Forms;
    using SystemViewer.Managers;

    using Microsoft.Win32;

    /// <summary>
    /// The open diagram command.
    /// </summary>
    public class OpenDiagramCommand : ICommand
    {
        #region Fields

        /// <summary>
        /// The dlg.
        /// </summary>
        private readonly OpenFileDialog dlg = new OpenFileDialog();

        #endregion

        #region Public Events

        /// <summary>
        /// The can execute changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The can execute.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void Execute(object parameter)
        {
            LayoutManager layoutmanager = ((MainWindow)parameter).Layoutmanager;
            layoutmanager.AddDiagram(this.GetLoadedDiagram, "");
        }
   


public DiagramCanvas GetLoadedDiagram(string diagramname)
{
   DiagramCanvas canvas = new DiagramCanvas();

    canvas.Open_Executed(this,null);
    return canvas;
}






    #endregion

        #region Methods

        /// <summary>
        /// The load canvas.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <returns>
        /// The <see cref="DiagramCanvas"/>.
        /// </returns>
        internal DiagramCanvas LoadCanvas(string filename)
        {
            FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read);
            var savedCanvas = XamlReader.Load(fs) as DiagramCanvas;
            fs.Close();
            return savedCanvas;
        }

        #endregion
    }
}