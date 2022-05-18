using School.Model.Models.SINCOdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model.General
{
    public class RetornoConsultarAsignaturas : RetornoGeneral
    {
        public List<Asignatura> ListAsignaturas { get; set; }
    }
}
