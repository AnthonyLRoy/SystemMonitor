// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmNewDiagram.xaml.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   Interaction lxaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Forms.dialogs
{
    using System.Windows;

    using SystemViewer.Helpers.Validatation;

    /// <summary>
    /// Interaction lxaml
    /// </summary>
    public partial class frmNewDiagram : Window
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="frmNewDiagram"/> class.
        /// </summary>
        public frmNewDiagram()
        {
            this.InitializeComponent();
            this.TxtDiagramName.Text = string.Empty;
            this.TxtDiagramName.Focus();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the diagram name.
        /// </summary>
        public string DiagramName
        {
            get
            {
                return this.TxtDiagramName.Text;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (FormValidation.IsValidDiagramName(this.TxtDiagramName.Text))
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show(
                    "the filename {0} is an invalid Diagram name", 
                    "Invalid file name", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// The cancel button_ on click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion
    }
}