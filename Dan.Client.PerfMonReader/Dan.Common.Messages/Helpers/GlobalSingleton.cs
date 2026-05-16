// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalSingleton.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The global singleton.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dan.Common.Messages.Helpers
{
    /// <summary>
    /// The global singleton.
    /// </summary>
    public static class GlobalSingleton
    {
        #region Static Fields

        /// <summary>
        /// The _ unique int.
        /// </summary>
        private static int _UniqueInt;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get next int value.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetNextIntValue()
        {
            return _UniqueInt++;
        }

        #endregion
    }
}