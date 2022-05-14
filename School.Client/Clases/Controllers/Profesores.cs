using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Client.Clases.Controllers
{
    public class ProfesoresOut : RetornoGeneral
    {
        public List<DataProfesor> ListaProfesores { get; set; }
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
}