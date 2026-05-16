// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeHelper.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The type helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Common.Messages.Helpers
{
    using System;
    using System.Collections;

    /// <summary>
    /// The type helper.
    /// </summary>
    public static class TypeHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// The is enumerable.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static bool IsEnumerable(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type);
        }

        /// <summary>
        /// The is simple or nullable type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static bool IsSimpleOrNullableType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type);
            }

            return IsSimpleType(type);
        }

        /// <summary>
        /// The is simple type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static bool IsSimpleType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(Decimal)
                   || type == typeof(DateTime) || type == typeof(Guid);
        }

        #endregion
    }
}