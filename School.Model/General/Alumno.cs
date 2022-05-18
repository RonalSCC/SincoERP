using School.Model.Models.SINCOdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model.General
{

    public class ConsultarMateriasCalificadasIn {
        public int AlumnoID { get; set; }
    }    
    

    public class RetornoReporteCalificacionAlumnos : RetornoGeneral {
        public List<Vista_CalificacionAlumno> ListaCalificaciones { get; set; }
    }

    public class RetornoConsultarEstudiantes : RetornoGeneral
    {
        public List<Alumno> ListEstudiantes { get; set; }
    }

    public class RetornoConsultarMateriasCalificadas : RetornoGeneral
    {
        public List<Vista_CalificacionAlumno> ListaCalificaciones { get; set; }
    }

}
