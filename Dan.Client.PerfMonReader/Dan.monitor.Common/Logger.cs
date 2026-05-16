// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The logger.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.monitor.Common
{
    using System;

    using log4net;

    /// <summary>
    /// The logger.
    /// </summary>
    public class Logger
    {
        #region Fields

        /// <summary>
        /// The _log 4 net adapter.
        /// </summary>
        private readonly ILog _log4NetAdapter;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public Logger(Type type)
        {
            this._log4NetAdapter = LogManager.GetLogger(type);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The log debug.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void LogDebug(string message)
        {
            this._log4NetAdapter.Debug(message);
        }

        /// <summary>
        /// The log debug.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public void LogDebug(string message, Exception exception)
        {
            this._log4NetAdapter.Debug(message, exception);
        }

        /// <summary>
        /// The log error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void LogError(string message)
        {
            this._log4NetAdapter.Error(message);
        }

        /// <summary>
        /// The log error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public void LogError(string message, Exception exception)
        {
            this._log4NetAdapter.Error(message, exception);
        }

        /// <summary>
        /// The log fatal.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void LogFatal(string message)
        {
            this._log4NetAdapter.Fatal(message);
        }

        /// <summary>
        /// The log fatal.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public void LogFatal(string message, Exception exception)
        {
            this._log4NetAdapter.Fatal(message, exception);
        }

        /// <summary>
        /// The log info.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void LogInfo(string message)
        {
            this._log4NetAdapter.Info(message);
        }

        /// <summary>
        /// The log info.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public void LogInfo(string message, Exception exception)
        {
            this._log4NetAdapter.Info(message, exception);
        }

        /// <summary>
        /// The log warning.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void LogWarning(string message)
        {
            this._log4NetAdapter.Warn(message);
        }

        /// <summary>
        /// The log warning.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public void LogWarning(string message, Exception exception)
        {
            this._log4NetAdapter.Warn(message, exception);
        }

        #endregion
    }
}