// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SaveDiagramCommand.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The save diagram command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Infrastructure.Commands
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Markup;

    using SystemViewer.Controls;
    using SystemViewer.Forms;
    using SystemViewer.Managers;

    using Microsoft.Win32;

    /// <summary>
    /// The save diagram command.
    /// </summary>
    public class SaveDiagramCommand : ICommand
    {
        #region Public Events

        /// <summary>
        /// The can execute changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The save document.
        /// </summary>
        /// <param name="canvas">
        /// The canvas.
        /// </param>
        /// <param name="filename">
        /// The filename.
        /// </param>
        public static void SaveDocument(DiagramCanvas canvas,ExecutedRoutedEventArgs e)
        {


            canvas.Save_Executed(canvas,null);

        }

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
            LayoutManager layout = ((MainWindow)parameter).Layoutmanager;

            var diagram = layout.GetActiveDiagram();
            if (diagram == null)
            {
                return;
            }

            //var saveFileDialog = new SaveFileDialog {
            //                             FileName = diagram.Name, 
            //                             AddExtension = true, 
            //                             Filter = "Text file (*.Diag)|*.Diag"
            //                         };

            //if (saveFileDialog.ShowDialog() == true)
            //{
            //    try
            //    {
                    //var filename = saveFileDialog.FileName;
                    SaveDocument(diagram, null);
                    MessageBox.Show(string.Format("Diagram {0} Saved", diagram.Name), "Save Document", MessageBoxButton.OK);
                //}
                //catch (Exception exception)
                //{
                //    MessageBox.Show("Failed To Save Document", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
            }
        }

        #endregion
    }
