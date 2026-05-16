using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventReceiver.Processors
{
    using System.Data;
    using System.Data.SqlClient;

    using Microsoft.WindowsAzure;

    internal class ErrorRecorder : Exception
    {

        internal void RecordError(Exception e, Microsoft.ServiceBus.Messaging.BrokeredMessage brokeredmessage)
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
                    cmd.Parameters.AddWithValue("@MessageText", brokeredmessage.GetBody<string>());
                    cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@errorType", e.Message);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ee)
            {
                throw new Exception("Fuck all Database failures", ee);
            }
        }
    }
}

