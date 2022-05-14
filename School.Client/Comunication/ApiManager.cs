using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using School.Client.Clases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace School.Client.Comunication
{
    public static class ApiManager
    {
        public static async Task<object> CallApiPost(string jsonObject, string baseUrl, string method)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("cache-control", "no-cache");
                    client.DefaultRequestHeaders.Add("JSON", "postValues");
                    using (StringContent jsonContent = new StringContent(jsonObject, Encoding.UTF8, "application/json"))
                    {
                        using (HttpResponseMessage response = await client.PostAsync($"{baseUrl}{method}", jsonContent))
                        {
                            try
                            {
                                var contentStream = await response.Content.ReadAsStreamAsync();
                                string contentText;
                                using (var streamReader = new StreamReader(contentStream))
                                {
                                    contentText = streamReader.ReadToEnd();
                                }

                                var result = contentText.FromJson<object>();
                                return result;
                            }
                            catch (Exception ex)
                            {
                                return "Ha ocurrido un error API";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Ha ocurrido un error CATCH";
            }
        }
    }
}