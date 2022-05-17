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
    public class ProfesorController : ControllerBase
    {

        public IConfiguration configuration;
        public string connectionLogs;
        public string connectiondb;

        public ProfesorController(IConfiguration iconfig)
        {
            configuration = iconfig;
            connectiondb = configuration.GetValue<string>("KeysConfiguration:DbConnection");
            connectionLogs = configuration.GetValue<string>("KeysConfiguration:DbConnectionLogs");
        }

        [HttpPost("ConsultarProfesores")]
        [HttpGet("ConsultarProfesores")]
        public ActionResult ConsultarProfesores()
        {
            RetornoConsultaProfesores Retorno = new RetornoConsultaProfesores();
            string nomapi = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                DbContextOptions<SINCOdbContext> SIESdbOptions = ConnectionSettings.SINCOdb(connectiondb);
                using (SINCOdbContext db = new SINCOdbContext(SIESdbOptions))
                {
                    var ListProfesores = db.Profesor.ToList();
                    db.SaveChanges();

                    Retorno.TransaccionID = 1;
                    Retorno.Descripcion = "Transacción exitosa";
                    Retorno.ListProfesores = ListProfesores;
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
        [HttpPost("CrearProfesor")]
        [HttpGet("CrearProfesor")]
        public ActionResult CrearProfesor(Profesor data)
        {
            RetornoGeneral Retorno = new RetornoGeneral();
            string nomapi = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                using (SINCOdbContext db = new SINCOdbContext()) {

                    #region Validaciones
                    if (data == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se han especificado datos para el registro";
                        goto outResponse;
                    }

                    var objProfesorExists = db.Profesor.FirstOrDefault(x => x.Identificacion == data.Identificacion);
                    if (objProfesorExists != null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "Ya existe un profesor registrado con este número de identificación";
                        goto outResponse;
                    }
                    #endregion

                    var InsertProfesor = new Profesor
                    {
                        Identificacion = data.Identificacion,
                        Activo = data.Activo,
                        Apellido = data.Apellido,
                        Direccion = data.Direccion,
                        Edad = data.Edad,
                        Nombre = data.Nombre,
                        Telefono = data.Telefono
                    };

                    db.Profesor.Add(InsertProfesor);
                    db.SaveChanges();

                    Retorno.TransaccionID = 1;
                    Retorno.Descripcion = "Transacción exitosa";
                    Retorno.Codigo = InsertProfesor.ProfesorID;
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
                    JsonEntrada = data.ToJson(),
                    JsonSalida = ex.ToJson(),
                    FechaRegistro = DateTime.Now,
                    DetalleTransaccion = "Ha ocurrido un error en la ejecución del servicio",
                    Error = true
                });
                return BadRequest(Retorno.ToJson());
            }
        }

        [HttpPost("EditarProfesor")]
        [HttpGet("EditarProfesor")]
        public ActionResult EditarProfesor(Profesor data)
        {
            RetornoGeneral Retorno = new RetornoGeneral();
            string nomapi = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                using (SINCOdbContext db = new SINCOdbContext())
                {

                    #region Validaciones
                    if (data == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se han especificado datos para la edición";
                        goto outResponse;
                    }

                    var objProfesorEdit = db.Profesor.FirstOrDefault(x => x.ProfesorID == data.ProfesorID);
                    if (objProfesorEdit == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se ha encontrado información del profesor a actualizar";
                        goto outResponse;
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(data.Identificacion)){ objProfesorEdit.Identificacion = data.Identificacion; }
                    if (!string.IsNullOrEmpty(data.Nombre)){ objProfesorEdit.Identificacion = data.Identificacion; }
                    if (!string.IsNullOrEmpty(data.Apellido)){ objProfesorEdit.Identificacion = data.Identificacion; }
                    if (!string.IsNullOrEmpty(data.Direccion)){ objProfesorEdit.Identificacion = data.Identificacion; }
                    if (!string.IsNullOrEmpty(data.Telefono)){ objProfesorEdit.Identificacion = data.Identificacion; }
                    if (data.Edad != 0){ objProfesorEdit.Edad = data.Edad; }
                    objProfesorEdit.Activo = data.Activo;

                    db.SaveChanges();

                    Retorno.TransaccionID = 1;
                    Retorno.Descripcion = "Transacción exitosa";
                    Retorno.Codigo = objProfesorEdit.ProfesorID;
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
                    JsonEntrada = data.ToJson(),
                    JsonSalida = ex.ToJson(),
                    FechaRegistro = DateTime.Now,
                    DetalleTransaccion = "Ha ocurrido un error en la ejecución del servicio",
                    Error = true
                });
                return BadRequest(Retorno.ToJson());
            }
        }

        [HttpPost("AsignarMateriasProfesor")]
        [HttpGet("AsignarMateriasProfesor")]
        public ActionResult AsignarMateriasProfesor(ProfesorAsignatura data)
        {
            RetornoGeneral Retorno = new RetornoGeneral();
            string nomapi = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                using (SINCOdbContext db = new SINCOdbContext())
                {

                    #region Validaciones
                    if (data == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se han especificado datos para la asignación de materias";
                        goto outResponse;
                    }

                    var objProfesor = db.Profesor.FirstOrDefault(x => x.ProfesorID == data.ProfesorID);
                    if (objProfesor == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se ha encontrado información del profesor";
                        goto outResponse;
                    }

                    var objAsignatura = db.Asignatura.FirstOrDefault(x => x.AsignaturaID == data.AsignaturaID);
                    if (objAsignatura == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se ha encontrado información de la asignatura";
                        goto outResponse;
                    }

                    var objAnioAcademico = db.AnioAcademico.FirstOrDefault(x => x.AnioAcademicoID == data.AnioAcademicoID);
                    if (objAnioAcademico == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se ha encontrado información del año academico";
                        goto outResponse;
                    }

                    var validateAsignaturaAsignada = db.ProfesorAsignatura.FirstOrDefault(x => x.AsignaturaID == data.AsignaturaID && x.AnioAcademicoID == data.AnioAcademicoID && x.Activo == true);
                    if (validateAsignaturaAsignada != null) {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "La asignatura ya se encuentra asignada a otro profesor";
                        goto outResponse;
                    }
                    #endregion

                    var InsertAsignacion = new ProfesorAsignatura
                    {
                        ProfesorID = data.ProfesorID,
                        AsignaturaID = data.AsignaturaID,
                        Activo = data.Activo,
                        AnioAcademicoID = data.AnioAcademicoID
                    };

                    db.ProfesorAsignatura.Add(InsertAsignacion);
                    db.SaveChanges();

                    Retorno.TransaccionID = 1;
                    Retorno.Descripcion = "Transacción exitosa";
                    Retorno.Codigo = InsertAsignacion.ProfesorID;
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
                    JsonEntrada = data.ToJson(),
                    JsonSalida = ex.ToJson(),
                    FechaRegistro = DateTime.Now,
                    DetalleTransaccion = "Ha ocurrido un error en la ejecución del servicio",
                    Error = true
                });
                return BadRequest(Retorno.ToJson());
            }
        }

        [HttpPost("EditarAsignacionMateriasProfesor")]
        [HttpGet("EditarAsignacionMateriasProfesor")]
        public ActionResult EditarAsignacionMateriasProfesor(ProfesorAsignatura data)
        {
            RetornoGeneral Retorno = new RetornoGeneral();
            string nomapi = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                using (SINCOdbContext db = new SINCOdbContext())
                {

                    #region Validaciones
                    if (data == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se han especificado datos de la asignación de materias";
                        goto outResponse;
                    }

                    var objProfesorAsignaturaEdit = db.ProfesorAsignatura.FirstOrDefault(x => x.ProfesorAsignaturaID == data.ProfesorAsignaturaID);
                    if (objProfesorAsignaturaEdit == null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se ha encontrado información de la asignatura para el profesor";
                        goto outResponse;
                    }

                    #endregion

                    objProfesorAsignaturaEdit.Activo = data.Activo;
                    db.SaveChanges();

                    Retorno.TransaccionID = 1;
                    Retorno.Descripcion = "Transacción exitosa";
                    Retorno.Codigo = objProfesorAsignaturaEdit.ProfesorID;
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
                    JsonEntrada = data.ToJson(),
                    JsonSalida = ex.ToJson(),
                    FechaRegistro = DateTime.Now,
                    DetalleTransaccion = "Ha ocurrido un error en la ejecución del servicio",
                    Error = true
                });
                return BadRequest(Retorno.ToJson());
            }
        }
    }
}
