// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringParameter.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The parameter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Parameters
{
    using StatsWebRole.Parameters.Parameter;

    /// <summary>
    /// The parameter.
    /// </summary>
    /// <typeparam name="TIn">
    /// </typeparam>
    /// <typeparam name="TOut">
    /// </typeparam>
    public class Parameter<TIn, TOut> : BaseParameter, IParameter
    {
        #region Fields

        /// <summary>
        /// The _value.
        /// </summary>
        private readonly TIn _value;

        /// <summary>
        /// The _translated value.
        /// </summary>
        private TOut _translatedValue;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter{TIn,TOut}"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public Parameter(string name, TIn value)
        {
            this.Name = name;
            this._value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether is translated.
        /// </summary>
        public bool IsTranslated
        {
            get
            {
                return this.Translated;
            }
        }

        /// <summary>
        /// Gets a value indicating whether is validated.
        /// </summary>
        public bool IsValidated
        {
            get
            {
                return this.Validated;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the translated value.
        /// </summary>
        public object TranslatedValue
        {
            get
            {
                return this._translatedValue;
            }

            set
            {
                this._translatedValue = (TOut)value;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public object Value
        {
            get
            {
                return this._value;
            }
        }

        #endregion
    }
}