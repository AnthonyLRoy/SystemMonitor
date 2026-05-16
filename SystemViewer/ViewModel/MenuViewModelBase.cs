// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuViewModelBase.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The view model base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.ViewModel
{
    using System.ComponentModel;

    /// <summary>
    /// The view model base.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Public Events

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion
    }
}