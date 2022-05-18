using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Client.Clases.Controllers
{

    public class DataAlumno
    {
        public int AlumnoID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Identificacion { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
    }

    public class AlumnoOut : RetornoGeneral
    {
        public List<DataAlumno> ListEstudiantes { get; set; }
    }

    public class RetornoConsultarMateriasCalificadas : RetornoGeneral
    {
        public List<DataCalificacionAlumno> ListaCalificaciones { get; set; }
    }

    public class DataCalificacionAlumno
    {
        public int AlumnoAsignaturaID { get; set; }
        public int AlumnoID { get; set; }
        public string NombreAlumno { get; set; }
        public string ApellidoAlumno { get; set; }
        public string IdentificacionAlumno { get; set; }
        public int? EdadAlumno { get; set; }
        public int AsignaturaID { get; set; }
        public string Asignatura { get; set; }
        public int? CodigoAsignatura { get; set; }
        public int AnioAcademico { get; set; }
        public int? Anio { get; set; }
        public int? ProfesorID { get; set; }
        public string IdentificacionProfesor { get; set; }
        public string NombreProfesor { get; set; }
        public decimal Calificacion { get; set; }
        public int Aprobo { get; set; }
    }

    public class CalificarAsignaturaEstudianteIn
    {
        public int AlumnoID { get; set; }
        public int AsignaturaID { get; set; }
        public int AnioAcademico { get; set; }
        public decimal Calificacion { get; set; }
    }

    public class ReporteOut : RetornoGeneral {
        public List<CalificacionAlumno> ListaCalificaciones { get; set; }
    }

    public class CalificacionAlumno {
        public int AlumnoAsignaturaID { get; set; }
        public int AlumnoID { get; set; }
        public string NombreAlumno { get; set; }
        public string ApellidoAlumno { get; set; }
        public string IdentificacionAlumno { get; set; }
        public int? EdadAlumno { get; set; }
        public int AsignaturaID { get; set; }
        public string Asignatura { get; set; }
        public int? CodigoAsignatura { get; set; }
        public int AnioAcademico { get; set; }
        public int? Anio { get; set; }
        public int? ProfesorID { get; set; }
        public string IdentificacionProfesor { get; set; }
        public string NombreProfesor { get; set; }
        public decimal Calificacion { get; set; }
        public int Aprobo { get; set; }
    } 
}