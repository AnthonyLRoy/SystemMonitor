// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorControlAttribute.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The monitor control attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.monitor.Common

{
    /// <summary>
    /// The monitor control attribute.
    /// </summary>
    public class MonitorControlAttribute : System.Attribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorControlAttribute"/> class.
        /// </summary>
        /// <param name="isinuse">
        /// The isinuse.
        /// </param>
        public MonitorControlAttribute(bool isinuse)
        {
            this.IsInUse = isinuse;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether is in use.
        /// </summary>
        public bool IsInUse { get; set; }

        #endregion
    }
}