using System;
using System.Collections.Generic;

namespace School.Model.Models.SINCOdb
{
    public partial class AlumnoAsignatura
    {
        public int AlumnoAsignaturaID { get; set; }
        public int AlumnoID { get; set; }
        public int AsignaturaID { get; set; }
        public int AnioAcademico { get; set; }
        public decimal Calificacion { get; set; }
    }
}
