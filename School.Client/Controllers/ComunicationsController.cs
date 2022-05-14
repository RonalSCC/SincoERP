using School.Client.Clases;
using School.Client.Comunication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace School.Client.Controllers
{
    public class ComunicationsController : Controller
    {
        static string URLServices = ConfigurationManager.AppSettings["URLServices"];
        // GET: Comunications
        public async Task<object> ConsultarProfesores(object data)
        {
            return await ApiManager.CallApiPost(data.ToJson(), URLServices, $"ConsultarProfesores");
        }
    }
}