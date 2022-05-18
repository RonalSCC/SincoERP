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
    public class AsignaturaController : ControllerBase
    {
        public IConfiguration configuration;
        public string connectionLogs;
        public string connectiondb;

        public AsignaturaController(IConfiguration iconfig)
        {
            configuration = iconfig;
            connectiondb = configuration.GetValue<string>("KeysConfiguration:DbConnection");
            connectionLogs = configuration.GetValue<string>("KeysConfiguration:DbConnectionLogs");
        }

        [HttpPost("ConsultarAsignaturas")]
        [HttpGet("ConsultarAsignaturas")]
        public ActionResult ConsultarAsignaturas()
        {
            RetornoConsultarAsignaturas Retorno = new RetornoConsultarAsignaturas();
            string nomapi = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                DbContextOptions<SINCOdbContext> SIESdbOptions = ConnectionSettings.SINCOdb(connectiondb);
                using (SINCOdbContext db = new SINCOdbContext(SIESdbOptions))
                {
                    var ListAsignaturas = db.Asignatura.ToList();
                    db.SaveChanges();

                    Retorno.TransaccionID = 1;
                    Retorno.Descripcion = "Transacción exitosa";
                    Retorno.ListAsignaturas = ListAsignaturas;
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

        [HttpPost("CrearAsignatura")]
        [HttpGet("CrearAsignatura")]
        public ActionResult CrearAsignatura(Asignatura data)
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
                        Retorno.Descripcion = "No se ha especificado la información para el registro de la asignatura";
                        goto outResponse;
                    }

                    var validateExists = db.Asignatura.FirstOrDefault(x => x.Codigo == data.Codigo);
                    if (validateExists != null)
                    {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "Ya existe una asignatura con este Codigo, por favor verifique información";
                        goto outResponse;
                    }
                    #endregion

                    var InsertAsignatura = new Asignatura
                    {
                        Nombre = data.Nombre,
                        Codigo = data.Codigo,
                        Activo = data.Activo
                    };

                    db.Asignatura.Add(InsertAsignatura);
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

        [HttpPost("EditarAsignatura")]
        [HttpGet("EditarAsignatura")]
        public ActionResult EditarAsignatura(Asignatura data)
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
                        Retorno.Descripcion = "No se ha especificado la información para el registro de la asignatura";
                        goto outResponse;
                    }

                    var objAsignatura = db.Asignatura.FirstOrDefault(x => x.AsignaturaID == data.AsignaturaID);
                    if (objAsignatura == null) {
                        Retorno.TransaccionID = 0;
                        Retorno.Descripcion = "No se ha encontrado información de la asignatura";
                        goto outResponse;
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(data.Nombre)) { objAsignatura.Nombre = data.Nombre; }
                    objAsignatura.Codigo = data.Codigo; 
                    objAsignatura.Activo = data.Activo; 

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
    }
}
