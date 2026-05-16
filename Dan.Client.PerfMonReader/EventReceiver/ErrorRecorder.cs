// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorRecorder.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The error recorder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace EventReceiver
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;

    using Microsoft.ServiceBus.Messaging;
    using Microsoft.WindowsAzure;

    /// <summary>
    /// The error recorder.
    /// </summary>
    public class ErrorRecorder
    {
        // write the unexpected Error to the database 
        #region Methods

        /// <summary>
        /// Writes an Error the the Database Error Table
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        internal void WriteError(Exception exception)
        {
            this.WriteToDataTable(exception.Message, "exception");
        }

        /// <summary>
        /// The write error.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="brokeredmessage">
        /// The brokeredmessage.
        /// </param>
        internal void WriteError(Exception exception, BrokeredMessage brokeredmessage)
        {
            this.WriteToDataTable(brokeredmessage.GetBody<string>(), "Message");
        }

        /// <summary>
        /// The write to data table.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        private void WriteToDataTable(string message, string type)
        {
            try
            {
                using (var connection = new SqlConnection(CloudConfigurationManager.GetSetting("dbConnectionString")))
                {
                    var cmd = new SqlCommand("sp_AddError")
                                  {
                                      CommandType = CommandType.StoredProcedure, 
                                      Connection = connection
                                  };
                    cmd.Parameters.AddWithValue("@MessageText", message);
                    cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ErrorType", type);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(string.Format("Error Access Error database {0}", e.Message));

                // do not through non essential
            }
        }

        #endregion
    }
}