using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Model.General;
using School.Model.Helpers;
using School.Model.Models;
using School.Model.Models.Context;
using School.Model.Models.SINCOdb;
using School.Model.Models.SINCOdbLogs;

namespace Sinco.School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        public IConfiguration configuration;
        public string connectionLogs;
        public string connectiondb;

        public AlumnoController(IConfiguration iconfig)
        {
            configuration = iconfig;
            connectiondb = configuration.GetValue<string>("KeysConfiguration:DbConnection");
            connectionLogs = configuration.GetValue<string>("KeysConfiguration:DbConnectionLogs");
        }

        [HttpPost("ConsultarEstudiantes")]
        [HttpGet("ConsultarEstudiantes")]
        public ActionResult ConsultarEstudiantes()
        {
            RetornoConsultarEstudiantes Retorno = new RetornoConsultarEstudiantes();
            string nomapi = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                DbContextOptions<SINCOdbContext> SIESdbOptions = ConnectionSettings.SINCOdb(connectiondb);
                using (SINCOdbContext db = new SINCOdbContext(SIESdbOptions))
                {
                    var ListAlumnos = db.Alumno.ToList();
                    db.SaveChanges();

                    Retorno.TransaccionID = 1;
                    Retorno.Descripcion = "Transacción exitosa";
                    Retorno.ListEstudiantes = ListAlumnos;
                }

            outResponse:
                return Ok(Retorno.ToJson());

            }
            catch (Exception ex)
            {
                Retorno.TransaccionID = 0;
                Retorno.Descripcion = "Ha ocurrido un error en la ejecución del servicio";
                Complement.SaveLog(new LogWebApi
                {
                    API = nomapi,
                    JsonEntrada = null,
                    JsonSalida = ex.ToJson(),
                    FechaRegistro = DateTime.Now,
                    DetalleTransaccion = "Ha ocurrido un error en la ejecución del servicio",
                    Error = true
                }, connectionLogs);
                return BadRequest(Retorno.ToJson());
            }
        }

        [HttpPost("CrearAlumno")]
        [HttpGet("CrearAlumno")]
        public ActionResult CrearAlumno(Alumno data)
        {
            RetornoGeneral Retorno = new RetornoGeneral();
            string nomapi = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                DbContextOptions<SINCOdbContext> SIESdbOptions = ConnectionSettings.SINCOdb(connectiondb);
                using (SINCOdbContext db = new SINCOdbContext(SIESdbOptions))
                {
                    #region Validaciones
                    if (data == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se ha especificado la información para el registro del alumno";
                        goto outResponse;
                    }

                    var validateExists = db.Alumno.FirstOrDefault(x => x.Identificacion.Equals(data.Identificacion));
                    if (validateExists != null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "Ya existe un alumno registrado con este número de identificación";
                        goto outResponse;
                    }
                    #endregion

                    var InsertAlumno = new Alumno
                    {
                        Nombre = data.Nombre,
                        Apellido = data.Apellido,
                        Identificacion = data.Identificacion,
                        Edad = data.Edad,
                        Direccion = data.Direccion,
                        Telefono = data.Telefono,
                        Activo = data.Activo
                    };

                    db.Alumno.Add(InsertAlumno);
                    db.SaveChanges();

                    Retorno.TransaccionID = 1;
                    Retorno.Descripcion = "Transacción exitosa";
                }

            outResponse:
                return Ok(Retorno.ToJson());

            }
            catch (Exception ex)
            {
                Retorno.TransaccionID = 0;
                Retorno.Descripcion = "Ha ocurrido un error en la creación del alumno";

                Complement.SaveLog(new LogWebApi
                {
                    API = nomapi,
                    JsonEntrada = null,
                    JsonSalida = ex.ToJson(),
                    FechaRegistro = DateTime.Now,
                    DetalleTransaccion = "Ha ocurrido un error en la ejecución del servicio",
                    Error = true
                }, connectionLogs);

                return BadRequest(Retorno.ToJson());
            }
        }

        [HttpPost("EditarAlumno")]
        [HttpGet("EditarAlumno")]
        public ActionResult EditarAlumno(Alumno data)
        {
            RetornoGeneral Retorno = new RetornoGeneral();
            string nomapi = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                DbContextOptions<SINCOdbContext> SIESdbOptions = ConnectionSettings.SINCOdb(connectiondb);
                using (SINCOdbContext db = new SINCOdbContext(SIESdbOptions))
                {
                    #region Validaciones
                    if (data == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se ha especificado la información para el registro del alumno";
                        goto outResponse;
                    }

                    var AlumnoEdit = db.Alumno.FirstOrDefault(x => x.AlumnoID == data.AlumnoID);
                    if (AlumnoEdit == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se han encontrado datos del alumno a editar";
                        goto outResponse;
                    }

                    var ValidateChangeDocumento = db.Alumno.FirstOrDefault(x => x.Identificacion.Equals(data.Identificacion) && x.AlumnoID != AlumnoEdit.AlumnoID);
                    if (ValidateChangeDocumento != null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "La actualización no se puede realizar ya que existe un alumno con el número de identificación";
                        goto outResponse;
                    }

                    if (AlumnoEdit.Activo == true && data.Activo == false)
                    {
                        var cantidadAsignaturasAlumno = db.AlumnoAsignatura.Where(x => x.AlumnoID == AlumnoEdit.AlumnoID).Count();
                        if (cantidadAsignaturasAlumno > 0) {
                            Retorno.TransaccionID = 0;
                            Retorno.Descripcion = "No se puede inactivar un alumno con materias calificadas";
                            goto outResponse;
                        }
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(data.Nombre)) { AlumnoEdit.Nombre = data.Nombre; }
                    if (!string.IsNullOrEmpty(data.Apellido)) { AlumnoEdit.Apellido = data.Apellido; }
                    if (!string.IsNullOrEmpty(data.Identificacion)) { AlumnoEdit.Identificacion = data.Identificacion; }
                    if (!string.IsNullOrEmpty(data.Direccion)) { AlumnoEdit.Direccion = data.Direccion; }
                    if (!string.IsNullOrEmpty(data.Telefono)) { AlumnoEdit.Telefono = data.Telefono; }
                    if (data.Edad != 0) { AlumnoEdit.Edad = data.Edad; }
                    AlumnoEdit.Activo = data.Activo;

                    db.SaveChanges();

                    Retorno.TransaccionID = 1;
                    Retorno.Descripcion = "Transacción exitosa";
                }

            outResponse:
                return Ok(Retorno.ToJson());

            }
            catch (Exception ex)
            {
                Retorno.TransaccionID = 0;
                Retorno.Descripcion = "Ha ocurrido un error en la ediación del alumno";

                Complement.SaveLog(new LogWebApi
                {
                    API = nomapi,
                    JsonEntrada = null,
                    JsonSalida = ex.ToJson(),
                    FechaRegistro = DateTime.Now,
                    DetalleTransaccion = "Ha ocurrido un error en la ejecución del servicio",
                    Error = true
                }, connectionLogs);

                return BadRequest(Retorno.ToJson());
            }
        }

        [HttpPost("CalificarAlumno")]
        [HttpGet("CalificarAlumno")]
        public ActionResult CalificarAlumno(AlumnoAsignatura data)
        {
            RetornoGeneral Retorno = new RetornoGeneral();
            string nomapi = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                DbContextOptions<SINCOdbContext> SIESdbOptions = ConnectionSettings.SINCOdb(connectiondb);
                using (SINCOdbContext db = new SINCOdbContext(SIESdbOptions))
                {
                    #region Validaciones
                    if (data == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se ha especificado la información para calificar al alumno";
                        goto outResponse;
                    }

                    var objAlumno = db.Alumno.FirstOrDefault(x => x.AlumnoID == data.AlumnoID);
                    if (objAlumno == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se han encontrado datos del alumno";
                        goto outResponse;
                    }

                    var objAsignatura = db.Asignatura.FirstOrDefault(x => x.AsignaturaID == data.AsignaturaID);
                    if (objAsignatura == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se han encontrado datos de la asignatura";
                        goto outResponse;
                    }

                    var objAnioAcademico = db.AnioAcademico.FirstOrDefault(x => x.AnioAcademicoID == data.AnioAcademico);
                    if (objAnioAcademico == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se han encontrado datos del año academico";
                        goto outResponse;
                    }

                    var validateAlumnoAsignatura = db.AlumnoAsignatura.FirstOrDefault(x => x.AlumnoID == data.AlumnoID && x.AsignaturaID == data.AsignaturaID && x.AnioAcademico == data.AnioAcademico);
                    if (validateAlumnoAsignatura != null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "Ya se le ha asignado la asignatura al alumno para este año academico";
                        goto outResponse;
                    }

                    if (data.Calificacion < 0 && data.Calificacion > 5)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "La calificación no puede ser menor de 0 y mayor a 5";
                        goto outResponse;
                    }

                    var validateProfesorAsignatura = db.ProfesorAsignatura.FirstOrDefault(x => x.AsignaturaID == data.AsignaturaID && x.AnioAcademicoID == data.AnioAcademico);
                    if (validateProfesorAsignatura == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se puede calificar una materia que no tiene asignada un profesor";
                        goto outResponse;
                    }
                    #endregion

                    var InsertAlumnAsignatura = new AlumnoAsignatura
                    {
                        AlumnoID = data.AlumnoID,
                        AsignaturaID = data.AsignaturaID,
                        Calificacion = data.Calificacion,
                        AnioAcademico = data.AnioAcademico
                    };

                    db.AlumnoAsignatura.Add(InsertAlumnAsignatura);
                    db.SaveChanges();

                    Retorno.TransaccionID = 1;
                    Retorno.Descripcion = "Transacción exitosa";
                }

            outResponse:
                return Ok(Retorno.ToJson());

            }
            catch (Exception ex)
            {
                Retorno.TransaccionID = 0;
                Retorno.Descripcion = "Ha ocurrido un error en la ejecución del servicio";

                Complement.SaveLog(new LogWebApi
                {
                    API = nomapi,
                    JsonEntrada = null,
                    JsonSalida = ex.ToJson(),
                    FechaRegistro = DateTime.Now,
                    DetalleTransaccion = "Ha ocurrido un error en la ejecución del servicio",
                    Error = true
                }, connectionLogs);

                return BadRequest(Retorno.ToJson());
            }
        }

        [HttpPost("ConsultarReporteCalificacionAlumnos")]
        [HttpGet("ConsultarReporteCalificacionAlumnos")]
        public ActionResult ConsultarReporteCalificacionAlumnos()
        {
            RetornoReporteCalificacionAlumnos Retorno = new RetornoReporteCalificacionAlumnos();
            string nomapi = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                DbContextOptions<SINCOdbContext> SIESdbOptions = ConnectionSettings.SINCOdb(connectiondb);
                using (SINCOdbContext db = new SINCOdbContext(SIESdbOptions))
                {
                    var ListCalificaciones = db.Vista_CalificacionAlumno.ToList();

                    Retorno.ListaCalificaciones = ListCalificaciones;
                    Retorno.TransaccionID = 1;
                    Retorno.Descripcion = "Transacción exitosa";
                }

                return Ok(Retorno.ToJson());

            }
            catch (Exception ex)
            {
                Retorno.TransaccionID = 0;
                Retorno.Descripcion = "Ha ocurrido un error en la ejecución del servicio";
                Complement.SaveLog(new LogWebApi
                {
                    API = nomapi,
                    JsonEntrada = null,
                    JsonSalida = ex.ToJson(),
                    FechaRegistro = DateTime.Now,
                    DetalleTransaccion = "Ha ocurrido un error en la ejecución del servicio",
                    Error = true
                }, connectionLogs);

                return BadRequest(Retorno.ToJson());
            }
        }

        [HttpPost("ConsultarMateriasCalificadas")]
        [HttpGet("ConsultarMateriasCalificadas")]
        public ActionResult ConsultarMateriasCalificadas(ConsultarMateriasCalificadasIn data)
        {
            RetornoConsultarMateriasCalificadas Retorno = new RetornoConsultarMateriasCalificadas();
            string nomapi = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                DbContextOptions<SINCOdbContext> SIESdbOptions = ConnectionSettings.SINCOdb(connectiondb);
                using (SINCOdbContext db = new SINCOdbContext(SIESdbOptions))
                {
                    #region Validaciones
                    if (data == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se ha especificado información del alumno";
                        goto outResponse;
                    }

                    var objAlumno = db.Alumno.FirstOrDefault(x => x.AlumnoID == data.AlumnoID);
                    if (objAlumno == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se ha encontrado información del alumno";
                        goto outResponse;
                    }
                    #endregion

                    var ListCalificaciones = db.Vista_CalificacionAlumno.Where(x => x.AlumnoID == data.AlumnoID).ToList();

                    Retorno.ListaCalificaciones = ListCalificaciones;
                    Retorno.TransaccionID = 1;
                    Retorno.Descripcion = "Transacción exitosa";
                }

                outResponse:
                return Ok(Retorno.ToJson());

            }
            catch (Exception ex)
            {
                Retorno.TransaccionID = 0;
                Retorno.Descripcion = "Ha ocurrido un error en la ejecución del servicio";
                Complement.SaveLog(new LogWebApi
                {
                    API = nomapi,
                    JsonEntrada = null,
                    JsonSalida = ex.ToJson(),
                    FechaRegistro = DateTime.Now,
                    DetalleTransaccion = "Ha ocurrido un error en la ejecución del servicio",
                    Error = true
                }, connectionLogs);

                return BadRequest(Retorno.ToJson());
            }
        }
    }
}
