using System;
using System.Collections.Generic;

namespace School.Model.Models.SINCOdb
{
    public partial class Vista_CalificacionAlumno
    {
        public int AlumnoAsignaturaID { get; set; }
        public int AlumnoID { get; set; }
        public string? NombreAlumno { get; set; }
        public string? ApellidoAlumno { get; set; }
        public string? IdentificacionAlumno { get; set; }
        public int? EdadAlumno { get; set; }
        public int AsignaturaID { get; set; }
        public string? Asignatura { get; set; }
        public int? CodigoAsignatura { get; set; }
        public int AnioAcademico { get; set; }
        public int? Anio { get; set; }
        public int? ProfesorID { get; set; }
        public string? IdentificacionProfesor { get; set; }
        public string? NombreProfesor { get; set; }
        public decimal Calificacion { get; set; }
        public int Aprobo { get; set; }
    }
}
