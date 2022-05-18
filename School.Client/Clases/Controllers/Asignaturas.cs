using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Client.Clases.Controllers
{
    public class DataAsignatura
    {
        public int AsignaturaID { get; set; }
        public string Nombre { get; set; }
        public int Codigo { get; set; }
        public bool Activo { get; set; }
    }

    public class AsignaturaOut : RetornoGeneral
    {
        public List<DataAsignatura> ListAsignaturas { get; set; }
    }
}