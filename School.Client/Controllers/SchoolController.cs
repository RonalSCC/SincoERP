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
            var Salida = new ProfesoresOut();
            try
            {
                var data =  await ApiManager.CallApiPost("",URLServices, $"Profesor/ConsultarProfesores");
                if (data != null)
                {
                    Salida = data.ToJson().FromJson<ProfesoresOut>();
                }
                else {
                    Salida.TransactionID = 0;
                    Salida.Descripcion = "El servicio no ha respondido correctamente";
                }
                return PartialView("Profesores", Salida);
            }
            catch (Exception ex)
            {
                Salida.TransactionID = 0;
                Salida.Descripcion = "Ha ocurrido un error en el consumo del servicio";
                return PartialView("Profesores", Salida);
            }
            
        }
    }
}