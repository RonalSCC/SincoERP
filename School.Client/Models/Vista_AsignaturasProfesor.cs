//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace School.Client.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vista_AsignaturasProfesor
    {
        public int ProfesorAsignaturaID { get; set; }
        public int ProfesorID { get; set; }
        public string NombreProfesor { get; set; }
        public string ApellidoProfesor { get; set; }
        public string IdentificacionProfesor { get; set; }
        public Nullable<int> EdadProfesor { get; set; }
        public int AsignaturaID { get; set; }
        public string NombreAsignatura { get; set; }
        public Nullable<int> CodigoAsignatura { get; set; }
        public int AnioAcademicoID { get; set; }
        public Nullable<int> AnioAcademico { get; set; }
        public Nullable<bool> Activo { get; set; }
    }
}
