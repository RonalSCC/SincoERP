using System;
using System.Collections.Generic;

namespace School.Model.Models.SINCOdbLogs
{
    public partial class LogWebApi
    {
        public int LogWebApiID { get; set; }
        public string API { get; set; } = null!;
        public string? JsonEntrada { get; set; }
        public string? JsonSalida { get; set; }
        public string DetalleTransaccion { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public bool? Error { get; set; }
    }
}
