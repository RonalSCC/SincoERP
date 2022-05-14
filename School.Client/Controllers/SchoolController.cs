using School.Client.Clases;
using School.Client.Clases.Controllers;
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
    public class SchoolController : Controller
    {
        static string URLServices = ConfigurationManager.AppSettings["URLServices"];

        // GET: School
        public ActionResult Inicio()
        {
            return View();
        }

        public async Task<ActionResult> Profesores()
        {
            try
            {
                var data =  await ApiManager.CallApiPost(new { }.ToJson(), URLServices, $"ConsultarProfesores");
                return PartialView("Profesores", data);
            }
            catch (Exception ex)
            {
                return PartialView("Profesores", new { 
                    TransactionID = 0,
                    Descripcion = "Ha ocurrido un error en el consumo del servicio (ApiManager)"
                });
            }
            
        }
    }
}