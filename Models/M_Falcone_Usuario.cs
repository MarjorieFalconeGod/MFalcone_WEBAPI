using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MFalcone_WEBAPI.Models
{
    public partial class M_FalconeUsuario
    {
        public M_FalconeUsuario()
        {
            M_FalconeSolicitudUsuarioCreadors = new HashSet<M_FalconeSolicitud>();
            M_FalconeSolicitudUsuarioGestors = new HashSet<M_FalconeSolicitud>();
        }

        public int Id { get; set; }
        public string Nombres { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public string Estado { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<M_FalconeSolicitud> M_FalconeSolicitudUsuarioCreadors { get; set; }
        [JsonIgnore]
        public virtual ICollection<M_FalconeSolicitud> M_FalconeSolicitudUsuarioGestors { get; set; }
    }
}
