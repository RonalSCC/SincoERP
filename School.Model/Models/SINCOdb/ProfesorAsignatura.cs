using System;
using System.Collections.Generic;

namespace School.Model.Models.SINCOdb
{
    public partial class ProfesorAsignatura
    {
        public int ProfesorAsignaturaID { get; set; }
        public int ProfesorID { get; set; }
        public int AsignaturaID { get; set; }
        public int AnioAcademicoID { get; set; }
        public bool? Activo { get; set; }
    }
}
