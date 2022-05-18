using School.Model.Models.SINCOdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model.General
{
    public class CrearProfesorIn
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
    }

    public class EditarProfesorIn {
        public int ProfesorID { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
    }

    public class AsignarMateriasProfesorIn {
        public int ProfesorID { get; set; }
        public int AsignaturaID { get; set; }
        public int AnioAcademicoID { get; set; }
        public bool Activo { get; set; }
    }

    public class EditarAsignacionMateriasProfesorIn {
        public int ProfesorAsignaturaID { get; set; }
        public bool Activo { get; set; }
    }

    public class RetornoConsultaProfesores : RetornoGeneral
    {
        public List<Profesor> ListProfesores { get; set; }
    }

    public class ConsultarMateriasProfesorIn
    {
        public int ProfesorID { get; set; }
    }

    public class RetornoConsultarMateriasProfesor : RetornoGeneral {
        public List<Asignatura> AsignaturasDisponibles { get; set; }
        public List<Vista_AsignaturasProfesor> AsignaturasAsignadas { get; set; }
    }
}
