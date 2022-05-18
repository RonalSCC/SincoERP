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
        static List<DataAlumno> ListAlumnos = null;
        static List<DataAsignatura> ListAsignaturas = null;
        static string URLServices = ConfigurationManager.AppSettings["URLServices"];
        // GET: School
        public ActionResult Inicio()
        {
            return View();
        }

        #region Views and Methods Profesor
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
        #endregion

        #region Views and Methods Student
        public ActionResult Estudiantes()
        {
            var Salida = new RetornoGeneral();
            try
            {
                Salida.TransaccionID = 1;
                Salida.Descripcion = "Transacción exitosa";
                return PartialView("Estudiantes", Salida);
            }
            catch (Exception ex)
            {
                Salida.TransaccionID = 0;
                Salida.Descripcion = "Ha ocurrido un error en el consumo del servicio";
                return PartialView("Estudiantes", Salida);
            }
        }

        public async Task<ActionResult> FormStudent(int AlumnoID = 0)
        {
            DataAlumno AlumnoReturn = new DataAlumno();
            try
            {
                if (AlumnoID != 0 && (ListAlumnos != null && ListAlumnos.Count > 0))
                {
                    var AlumnoSelected = ListAlumnos.FirstOrDefault(x => x.AlumnoID == AlumnoID);
                    if (AlumnoSelected != null)
                    {
                        AlumnoReturn = AlumnoSelected;
                    }
                }
                return PartialView("FormStudent", AlumnoReturn);
            }
            catch (Exception ex)
            {
                return PartialView("FormStudent", AlumnoReturn);
            }
        }

        public ActionResult GridEstudiantes()
        {
            AlumnoOut Salida = new AlumnoOut();
            try
            {
                Task.Run(async () =>
                {
                    var data = await ApiManager.CallApiPost("", URLServices, $"Alumno/ConsultarEstudiantes");
                    if (data != null)
                    {
                        Salida = data.ToJson().FromJson<AlumnoOut>();
                        if (Salida.ListEstudiantes != null && Salida.ListEstudiantes.Count > 0)
                        {
                            ListAlumnos = Salida.ListEstudiantes;
                        }
                    }
                    else
                    {
                        Salida.TransaccionID = 0;
                        Salida.Descripcion = "El servicio no ha respondido correctamente";
                    }
                }).Wait();
                return PartialView("_GridEstudiantes", Salida);
            }
            catch (Exception ex)
            {
                Salida.TransaccionID = 0;
                Salida.Descripcion = "Ha ocurrido un error en el consumo del servicio";
                return PartialView("_GridEstudiantes", Salida);
            }
        }

        [HttpPost]
        public async Task<JsonResult> CrearAlumno(DataAlumno data)
        {
            var Salida = new RetornoGeneral();
            try
            {
                var URL = "Alumno/CrearAlumno";
                if (data.AlumnoID != 0)
                {
                    URL = "Alumno/EditarAlumno";
                }
                var dataResponse = await ApiManager.CallApiPost(data.ToJson(), URLServices, $"{URL}");
                if (dataResponse != null)
                {
                    Salida = dataResponse.ToJson().FromJson<RetornoGeneral>();
                    if (Salida.TransaccionID == 0 && Salida.Descripcion == null)
                    {
                        Salida.Descripcion = "No se ha podido completar el registro";
                    }
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

        public async Task<ActionResult> AsignarMateriasEstudiante(int AlumnoID = 0)
        {
            RetornoConsultarMateriasCalificadas Salida = new RetornoConsultarMateriasCalificadas();
            try
            {
                if (AlumnoID != 0)
                {
                    ViewData["AlumnoID"] = AlumnoID;
                    Task.Run(async () =>
                    {
                        var data = await ApiManager.CallApiPost(new { AlumnoID = AlumnoID }.ToJson(), URLServices, $"Alumno/ConsultarMateriasCalificadas");
                        if (data != null)
                        {
                            Salida = data.ToJson().FromJson<RetornoConsultarMateriasCalificadas>();
                        }
                        else
                        {
                            Salida.TransaccionID = 0;
                            Salida.Descripcion = "El servicio no ha respondido correctamente";
                        }
                    }).Wait();
                }
                else
                {
                    Salida.TransaccionID = 0;
                    Salida.Descripcion = "No se ha especificado información del profesor seleccionado";
                }
                return PartialView("AsignarMateriasEstudiante", Salida);
            }
            catch (Exception ex)
            {
                Salida.TransaccionID = 0;
                Salida.Descripcion = "Ha ocurrido un error en el consumo del servicio";
                return PartialView("AsignarMateriasEstudiante", Salida);
            }
        }

        [HttpPost]
        public async Task<JsonResult> CalificarAsignaturaEstudiante(CalificarAsignaturaEstudianteIn data)
        {
            var Salida = new RetornoGeneral();
            try
            {
                var dataResponse = await ApiManager.CallApiPost(data.ToJson(), URLServices, $"Alumno/CalificarAlumno");
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
        #endregion

        #region Asignaturas
        public ActionResult Asignaturas()
        {
            var Salida = new RetornoGeneral();
            try
            {
                Salida.TransaccionID = 1;
                Salida.Descripcion = "Transacción exitosa";
                return PartialView("Asignaturas", Salida);
            }
            catch (Exception ex)
            {
                Salida.TransaccionID = 0;
                Salida.Descripcion = "Ha ocurrido un error en el consumo del servicio";
                return PartialView("Asignaturas", Salida);
            }
        }

        public async Task<ActionResult> FormAsignaturas(int AsignaturaID = 0)
        {
            DataAsignatura AsignaturaReturn = new DataAsignatura();
            try
            {
                if (AsignaturaID != 0 && (ListAsignaturas != null && ListAsignaturas.Count > 0))
                {
                    var AsignaturaSelect = ListAsignaturas.FirstOrDefault(x => x.AsignaturaID == AsignaturaID);
                    if (AsignaturaSelect != null)
                    {
                        AsignaturaReturn = AsignaturaSelect;
                    }
                }
                return PartialView("FormAsignaturas", AsignaturaReturn);
            }
            catch (Exception ex)
            {
                return PartialView("FormAsignaturas", AsignaturaReturn);
            }
        }

        public ActionResult GridAsignaturas()
        {
            AsignaturaOut Salida = new AsignaturaOut();
            try
            {
                Task.Run(async () =>
                {
                    var data = await ApiManager.CallApiPost("", URLServices, $"Asignatura/ConsultarAsignaturas");
                    if (data != null)
                    {
                        Salida = data.ToJson().FromJson<AsignaturaOut>();
                        if (Salida.ListAsignaturas != null && Salida.ListAsignaturas.Count > 0)
                        {
                            ListAsignaturas = Salida.ListAsignaturas;
                        }
                    }
                    else
                    {
                        Salida.TransaccionID = 0;
                        Salida.Descripcion = "El servicio no ha respondido correctamente";
                    }
                }).Wait();
                return PartialView("_GridAsignaturas", Salida);
            }
            catch (Exception ex)
            {
                Salida.TransaccionID = 0;
                Salida.Descripcion = "Ha ocurrido un error en el consumo del servicio";
                return PartialView("_GridAsignaturas", Salida);
            }
        }

        [HttpPost]
        public async Task<JsonResult> CrearAsignatura(DataAsignatura data)
        {
            var Salida = new RetornoGeneral();
            try
            {
                var URL = "Asignatura/CrearAsignatura";
                if (data.AsignaturaID != 0)
                {
                    URL = "Asignatura/EditarAsignatura";
                }
                var dataResponse = await ApiManager.CallApiPost(data.ToJson(), URLServices, $"{URL}");
                if (dataResponse != null)
                {
                    Salida = dataResponse.ToJson().FromJson<RetornoGeneral>();
                    if (Salida.TransaccionID == 0 && Salida.Descripcion == null)
                    {
                        Salida.Descripcion = "No se ha podido completar el registro";
                    }
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
        #endregion

        #region Reportes
        public ActionResult Reportes()
        {
            var Salida = new RetornoGeneral();
            try
            {
                Salida.TransaccionID = 1;
                Salida.Descripcion = "Transacción exitosa";
                return PartialView("Reportes", Salida);
            }
            catch (Exception ex)
            {
                Salida.TransaccionID = 0;
                Salida.Descripcion = "Ha ocurrido un error en el consumo del servicio";
                return PartialView("Reportes", Salida);
            }
        }

        public ActionResult GridReporte()
        {
            ReporteOut Salida = new ReporteOut();
            try
            {
                Task.Run(async () =>
                {
                    var data = await ApiManager.CallApiPost("", URLServices, $"Alumno/ConsultarReporteCalificacionAlumnos");
                    if (data != null)
                    {
                        Salida = data.ToJson().FromJson<ReporteOut>();
                    }
                    else
                    {
                        Salida.TransaccionID = 0;
                        Salida.Descripcion = "El servicio no ha respondido correctamente";
                    }
                }).Wait();
                return PartialView("_GridReporte", Salida);
            }
            catch (Exception ex)
            {
                Salida.TransaccionID = 0;
                Salida.Descripcion = "Ha ocurrido un error en el consumo del servicio";
                return PartialView("_GridReporte", Salida);
            }
        }

        #endregion
    }
}