using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Client.Clases
{
    public static class JsonMethods
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
    }
}