//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace School.Client.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vista_CalificacionAlumno
    {
        public int AlumnoAsignaturaID { get; set; }
        public int AlumnoID { get; set; }
        public string NombreAlumno { get; set; }
        public string ApellidoAlumno { get; set; }
        public string IdentificacionAlumno { get; set; }
        public Nullable<int> EdadAlumno { get; set; }
        public int AsignaturaID { get; set; }
        public string Asignatura { get; set; }
        public Nullable<int> CodigoAsignatura { get; set; }
        public int AnioAcademico { get; set; }
        public Nullable<int> Anio { get; set; }
        public Nullable<int> ProfesorID { get; set; }
        public string IdentificacionProfesor { get; set; }
        public string NombreProfesor { get; set; }
        public decimal Calificacion { get; set; }
        public int Aprobo { get; set; }
    }
}