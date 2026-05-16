using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dan.Common.Messages;
using Microsoft.WindowsAzure;

namespace EventReceiver.Data
{
    public class DataManager
    {
        public DataManager()
        {
        }

        internal Task UpdateRow(MonitorMessageCollection message)
        {
                using (var connection = new SqlConnection(CloudConfigurationManager.GetSetting("dbConnectionString")))
                {
                    var result = message.ToDataTable();
                    var cmd = new SqlCommand("sp_RefreshStatsCollection")
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = connection
                    };
                    cmd.Parameters.AddWithValue("@Events", result);
                    cmd.Parameters["@Events"].SqlDbType = SqlDbType.Structured;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            return null;
        }
    }
}


