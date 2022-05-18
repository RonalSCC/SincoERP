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
        static List<DataProfesor> ListProf = null;
        static string URLServices = ConfigurationManager.AppSettings["URLServices"];
        // GET: School
        public ActionResult Inicio()
        {
            return View();
        }

        public async Task<ActionResult> Profesores()
        {
            var Salida = new RetornoGeneral();
            try
            {
                Salida.TransaccionID = 1;
                Salida.Descripcion = "Transacción exitosa";
                return PartialView("Profesores", Salida);
            }
            catch (Exception ex)
            {
                Salida.TransaccionID = 0;
                Salida.Descripcion = "Ha ocurrido un error en el consumo del servicio";
                return PartialView("Profesores", Salida);
            }

        }

        public async Task<ActionResult> FormNewProfesor(int IDProfesor = 0)
        {
            DataProfesor ProfesorReturn = new DataProfesor();
            try
            {
                if (IDProfesor != 0 && (ListProf != null && ListProf.Count > 0))
                {
                    var ProfesorSelected = ListProf.FirstOrDefault(x => x.ProfesorID == IDProfesor);
                    if (ProfesorSelected != null) {
                        ProfesorReturn = ProfesorSelected;
                    }
                }
                return PartialView("FormNewProfesor", ProfesorReturn);
            }
            catch (Exception ex)
            {
                return PartialView("FormNewProfesor", ProfesorReturn);
            }
        }

        public ActionResult GridProfesores()
        {
            ProfesoresOut Salida = new ProfesoresOut();
            try
            {
                Task.Run(async () =>
                {
                    var data = await ApiManager.CallApiPost("", URLServices, $"Profesor/ConsultarProfesores");
                    if (data != null)
                    {
                        Salida = data.ToJson().FromJson<ProfesoresOut>();
                        if (Salida.ListProfesores != null && Salida.ListProfesores.Count > 0)
                        {
                            ListProf = Salida.ListProfesores;
                        }
                    }
                    else
                    {
                        Salida.TransaccionID = 0;
                        Salida.Descripcion = "El servicio no ha respondido correctamente";
                    }
                }).Wait();
                return PartialView("_GridProfesores", Salida);
            }
            catch (Exception ex)
            {
                Salida.TransaccionID = 0;
                Salida.Descripcion = "Ha ocurrido un error en el consumo del servicio";
                return PartialView("_GridProfesores", Salida);
            }
        }
        [HttpPost]
        public async Task<JsonResult> CrearProfesor(DataProfesor data)
        {
            var Salida = new RetornoGeneral();
            try
            {
                var URL = "Profesor/CrearProfesor";
                if (data.ProfesorID != 0)
                {
                    URL = "Profesor/EditarProfesor";
                }
                var dataResponse = await ApiManager.CallApiPost(data.ToJson(), URLServices, $"{URL}");
                if (dataResponse != null)
                {
                    Salida = dataResponse.ToJson().FromJson<RetornoGeneral>();
                }
                else
                {
                    Salida.TransaccionID = 0;
                    Salida.Descripcion = "El servicio no ha respondido correctamente";
                }

                return Json(Salida);
            }
            catch (Exception ex)
            {
                Salida.TransaccionID = 0;
                Salida.Descripcion = "Ha ocurrido un error en el consumo del servicio";
                return Json(Salida);
            }
        }

        public async Task<ActionResult> AsignarMateriasProfesor(int IDProfesor = 0)
        {
            RetornoConsultarMateriasProfesor Salida = new RetornoConsultarMateriasProfesor();
            try
            {
                if (IDProfesor != 0)
                {
                    ViewData["ProfesorID"] = IDProfesor;
                    Task.Run(async () =>
                    {
                        var data = await ApiManager.CallApiPost(new { ProfesorID = IDProfesor }.ToJson(), URLServices, $"Profesor/ConsultarMateriasProfesor");
                        if (data != null)
                        {
                            Salida = data.ToJson().FromJson<RetornoConsultarMateriasProfesor>();
                        }
                        else
                        {
                            Salida.TransaccionID = 0;
                            Salida.Descripcion = "El servicio no ha respondido correctamente";
                        }
                    }).Wait();
                }
                else {
                    Salida.TransaccionID = 0;
                    Salida.Descripcion = "No se ha especificado información del profesor seleccionado";
                }
                return PartialView("AsignarMateriasProfesor", Salida);
            }
            catch (Exception ex)
            {
                Salida.TransaccionID = 0;
                Salida.Descripcion = "Ha ocurrido un error en el consumo del servicio";
                return PartialView("AsignarMateriasProfesor", Salida);
            }
        }

        [HttpPost]
        public async Task<JsonResult> CrearAsignaturaProfesor(CrearAsignaturaProfesorIn data)
        {
            var Salida = new RetornoGeneral();
            try
            {
                var dataResponse = await ApiManager.CallApiPost(data.ToJson(), URLServices, $"Profesor/AsignarMateriasProfesor");
                if (dataResponse != null)
                {
                    Salida = dataResponse.ToJson().FromJson<RetornoGeneral>();
                }
                else
                {
                    Salida.TransaccionID = 0;
                    Salida.Descripcion = "El servicio no ha respondido correctamente";
                }

                return Json(Salida);
            }
            catch (Exception ex)
            {
                Salida.TransaccionID = 0;
                Salida.Descripcion = "Ha ocurrido un error en el consumo del servicio";
                return Json(Salida);
            }
        }
    }
}