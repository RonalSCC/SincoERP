using Microsoft.EntityFrameworkCore;
using School.Model.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model.Models
{
    public class ConnectionSettings
    {
        public static DbContextOptions<SINCOdbContext> SINCOdb
        {
            get
            {
                string defaultConnectionSIESdb = Environment.GetEnvironmentVariable("DbConnection");
                var optionsSIESdb = new DbContextOptionsBuilder<SINCOdbContext>();
                optionsSIESdb.UseSqlServer(defaultConnectionSIESdb);

                return optionsSIESdb.Options;
            }
        }
        public static DbContextOptions<SINCOdbLogsContext> SINCOdbLogs
        {
            get
            {
                string defaultConnectionSIESdbLogs = Environment.GetEnvironmentVariable("DbConnectionLogs");
                var optionsSIESdbLogs = new DbContextOptionsBuilder<SINCOdbLogsContext>();
                optionsSIESdbLogs.UseSqlServer(defaultConnectionSIESdbLogs);
                return optionsSIESdbLogs.Options;
            }
        }
    }
}
