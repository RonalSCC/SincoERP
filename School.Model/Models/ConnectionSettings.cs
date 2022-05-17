using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public static DbContextOptions<SINCOdbContext> SINCOdb(string connectionString)
        {
            var optionsSIESdb = new DbContextOptionsBuilder<SINCOdbContext>();
            optionsSIESdb.UseSqlServer(connectionString);

            return optionsSIESdb.Options;
        }
        public static DbContextOptions<SINCOdbLogsContext> SINCOdbLogs(string connectionStringLogs)
        {
            var optionsSIESdbLogs = new DbContextOptionsBuilder<SINCOdbLogsContext>();
            optionsSIESdbLogs.UseSqlServer(connectionStringLogs);
            return optionsSIESdbLogs.Options;
        }
    }
}
