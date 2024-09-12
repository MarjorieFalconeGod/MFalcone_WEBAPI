using System;
using System.Collections.Generic;

namespace MFalcone_WEBAPI.Models
{
    public partial class M_FalconeSolicitud
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string DescripcionSolicitud { get; set; } = null!;
        public string? Justificativo { get; set; }
        public string Estado { get; set; } = null!;
        public string? DetalleGestion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public DateTime? FechaGestion { get; set; }
        public int UsuarioCreadorId { get; set; }
        public int? UsuarioGestorId { get; set; }

        public virtual M_FalconeUsuario UsuarioCreador { get; set; } = null!;
        public virtual M_FalconeUsuario? UsuarioGestor { get; set; }
    }
}
