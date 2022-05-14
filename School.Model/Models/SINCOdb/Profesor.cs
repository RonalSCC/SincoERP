using System;
using System.Collections.Generic;

namespace School.Model.Models.SINCOdb
{
    public partial class Profesor
    {
        public int ProfesorID { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public int Edad { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public bool Activo { get; set; }
    }
}
