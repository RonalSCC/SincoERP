using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using School.Model.Models;
using School.Model.Models.Context;
using School.Model.Models.SINCOdbLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model.Helpers
{
    public static class Complement
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T FromJson<T>(this string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings
                {
                    DateFormatString = "dd-MM-yyyy HH:mm:ss tt",
                    Formatting = Newtonsoft.Json.Formatting.Indented
                });
            }
            catch (Exception ex)
            {
                return JArray.Parse(value).ToObject<T>(new JsonSerializer
                {
                    DateFormatString = "dd-MM-yyyy HH:mm:ss tt",
                    Formatting = Newtonsoft.Json.Formatting.Indented
                });
            }

        }

        public static int SaveLog(LogWebApi log, string connection = null) {

            var IDReturn = 0;
            try
            {
                DbContextOptions<SINCOdbLogsContext> SINCOdbLogsOptions = ConnectionSettings.SINCOdbLogs(connection);
                using (SINCOdbLogsContext db = new SINCOdbLogsContext(SINCOdbLogsOptions)) {

                    db.LogWebApi.Add(log);
                    IDReturn = log.LogWebApiID;
                }
                return IDReturn;
            }
            catch (Exception ex)
            {
                return IDReturn;
            }
        }
    }
}
