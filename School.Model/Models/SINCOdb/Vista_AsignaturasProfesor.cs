using System;
using System.Collections.Generic;

namespace School.Model.Models.SINCOdb
{
    public partial class Vista_AsignaturasProfesor
    {
        public int ProfesorAsignaturaID { get; set; }
        public int ProfesorID { get; set; }
        public string? NombreProfesor { get; set; }
        public string? ApellidoProfesor { get; set; }
        public string? IdentificacionProfesor { get; set; }
        public int? EdadProfesor { get; set; }
        public int AsignaturaID { get; set; }
        public string? NombreAsignatura { get; set; }
        public int? CodigoAsignatura { get; set; }
        public int AnioAcademicoID { get; set; }
        public int? AnioAcademico { get; set; }
        public bool? Activo { get; set; }
    }
}
