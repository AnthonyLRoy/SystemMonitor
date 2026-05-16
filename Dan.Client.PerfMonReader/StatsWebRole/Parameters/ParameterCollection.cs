// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterCollection.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The parameter collection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Parameters
{
    using System.Collections.Generic;

    using StatsWebRole.Exceptions;
    using StatsWebRole.Parameters.Parameter;
    using StatsWebRole.Translators;
    using StatsWebRole.Validators;

    /// <summary>
    /// The parameter collection.
    /// </summary>
    public class ParameterCollection : Dictionary<string, IParameter>, IParameterCollection
    {
        #region Fields

        /// <summary>
        /// The _translator.
        /// </summary>
        private readonly ITranslator _translator;

        /// <summary>
        /// The _validator.
        /// </summary>
        private readonly IDataValidator _validator;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterCollection"/> class.
        /// </summary>
        /// <param name="validator">
        /// The validator.
        /// </param>
        /// <param name="translator">
        /// The translator.
        /// </param>
        public ParameterCollection(IDataValidator validator, ITranslator translator)
        {
            this._validator = validator;
            this._translator = translator;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether is translated.
        /// </summary>
        public bool IsTranslated { get; private set; }

        /// <summary>
        /// Gets a value indicating whether is validated.
        /// </summary>
        public bool IsValidated { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The translate.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ErrorObject> Translate()
        {
            List<ErrorObject> errors = this._translator.Translate(this);
            this.IsTranslated = errors.Count == 0;
            return errors;
        }

        /// <summary>
        /// The validate.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ErrorObject> Validate()
        {
            List<ErrorObject> errors = this._validator.Validate(this);
            this.IsValidated = errors.Count == 0;
            return errors;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The validate and translate.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        internal List<ErrorObject> ValidateAndTranslate()
        {
            List<ErrorObject> results = this.Validate();
            results.AddRange(this.Translate());
            return results;
        }

        #endregion
    }
}