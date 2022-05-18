using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Client.Clases.Controllers
{
    public class ProfesoresOut : RetornoGeneral
    {
        public List<DataProfesor> ListProfesores { get; set; }
    }

    public class DataProfesor {
        public int ProfesorID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Identificacion { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
    }

    public class RetornoConsultarMateriasProfesor : RetornoGeneral
    {
        public List<AsignaturaData> AsignaturasDisponibles { get; set; }
        public List<AsignaturasProfesorData> AsignaturasAsignadas { get; set; }
    }

    public class AsignaturaData
    {
        public int AsignaturaID { get; set; }
        public string Nombre { get; set; }
        public int Codigo { get; set; }
        public bool Activo { get; set; }
    }

    public class AsignaturasProfesorData
    {
        public int ProfesorAsignaturaID { get; set; }
        public int ProfesorID { get; set; }
        public string NombreProfesor { get; set; }
        public string ApellidoProfesor { get; set; }
        public string IdentificacionProfesor { get; set; }
        public int? EdadProfesor { get; set; }
        public int AsignaturaID { get; set; }
        public string NombreAsignatura { get; set; }
        public int? CodigoAsignatura { get; set; }
        public int AnioAcademicoID { get; set; }
        public int? AnioAcademico { get; set; }
        public bool? Activo { get; set; }
    }

    public class CrearAsignaturaProfesorIn {
        public int ProfesorID { get; set; }
        public int AsignaturaID { get; set; }
        public int AnioAcademicoID { get; set; }
        public bool Activo { get; set; }
    }
}