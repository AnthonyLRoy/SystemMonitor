namespace StatsWebRole.Data.Commands
{
        public class ServiceStatistics : BaseCommand, IDataCommand
        {
            private const string CStoredProcedure = "dbo.sp_GetStatsByGroup";
            private const string CconnectionstringKey = "dbconnectionstring";

            public ServiceStatistics()
            {
                StroredProcedureName = CStoredProcedure;
                ConnectionStringKey = CconnectionstringKey;
            }
        }
    }
