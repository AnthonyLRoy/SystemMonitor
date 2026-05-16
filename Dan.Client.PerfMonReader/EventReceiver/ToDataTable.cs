using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dan.Common.Messages;

namespace EventReceiver
{
    public static class MessageExtensions
    {
        public static DataTable ToDataTable(this MonitorMessageCollection message)
        {
            DataTable dt = CreateDataTable();

            try
            {
                foreach (var em in message.Messages)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = em.Properties["ID"].ToString();
                    dr["TypeCode"] = int.Parse(em.Properties["TypeCode"].ToString());
                    dr["rawvalue"] = float.Parse(em.Properties["rawvalue"].ToString());
                    dr["calculatedValue"] = float.Parse(em.Properties["calculated"].ToString());
                    dr["TextMessage"] = em.Properties["TextMessage"].ToString();
                    dr["EventCreated"] = DateTime.Parse(em.Properties["created"].ToString());
                    dr["EventReceived"] = DateTime.Now;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception e)
            {

                throw e;
            }

                         

            return dt;
        }

        private static DataTable CreateDataTable()
        {
            DataTable eventTable = new DataTable("PerformanceData");
            eventTable.Columns.Add("ID",typeof(string));
            eventTable.Columns.Add("TypeCode", typeof(int));
            eventTable.Columns.Add("TextMessage", typeof(string));
            eventTable.Columns.Add("rawvalue",typeof(float));
            eventTable.Columns.Add("calculatedValue", typeof(float));
            eventTable.Columns.Add("EventCreated", typeof(DateTime));
            eventTable.Columns.Add("EventReceived", typeof(DateTime));
            return eventTable;
        }
    }

    
    
}
