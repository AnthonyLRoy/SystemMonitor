// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetricReaderAttribute.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The metric reader attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.monitor.Common
{
    using System;

    /// <summary>
    /// The metric reader attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MetricReaderAttribute : Attribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetricReaderAttribute"/> class.
        /// </summary>
        /// <param name="isActive">
        /// The is active.
        /// </param>
        public MetricReaderAttribute(bool isActive)
        {
            this.IsActive = isActive;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive { get; set; }

        #endregion
    };
}