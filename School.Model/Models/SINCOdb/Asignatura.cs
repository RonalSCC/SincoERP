using System;
using System.Collections.Generic;

namespace School.Model.Models.SINCOdb
{
    public partial class Asignatura
    {
        public int AsignaturaID { get; set; }
        public string? Nombre { get; set; }
        public int Codigo { get; set; }
        public bool Activo { get; set; }
    }
}
