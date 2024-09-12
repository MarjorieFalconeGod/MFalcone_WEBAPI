namespace MFalcone_WEBAPI.Models.DTO
{

    public class Login
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }

    }


    public class LoginResponse
    {
        public Boolean IsValid { get; set; }
        public string Rol { get; set; }
    }


    public class UsuarioDTO
    {
        public string Nombres { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public string Estado { get; set; } = null!;
    }

    public class SolicitudDTO
    {
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
    }


    public class ResponseDTO
    {
        public string CodError { get; set; }
        public string MensajeError { get; set; }

    }

}
