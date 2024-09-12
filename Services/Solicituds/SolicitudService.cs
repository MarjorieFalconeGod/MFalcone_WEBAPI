using Microsoft.EntityFrameworkCore;
using MFalcone_WEBAPI.Models;
using MFalcone_WEBAPI.Models.DTO;

namespace MFalcone_WEBAPI.Services.SolicitudServices
{
    public class SolicitudService : ISolicitudService
    {

        private M_Falcone_BDContext _context;

        public SolicitudService(M_Falcone_BDContext context)
        {
            _context = context;
        }

        public ResponseDTO ActualizarSolicitud(int id, SolicitudDTO solicitud)
        {
            var result = _context.Database.ExecuteSqlRaw("EXEC [dbo].[M-Falcone_ActualizarSolicitud] @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8",
            id, solicitud.Tipo, solicitud.DescripcionSolicitud, solicitud.Justificativo, solicitud.Estado,
            solicitud.DetalleGestion, solicitud.FechaIngreso, solicitud.UsuarioCreadorId, solicitud.UsuarioGestorId);

            if (result == 0)
            {
                return new ResponseDTO
                {
                    CodError = "0000",
                    MensajeError = "Solicitud Actualizada Correctamente"
                };
            }
            else
            {
                return new ResponseDTO
                {
                    CodError = "1111",
                    MensajeError = "Solicitud no encontrada"
                };
            }
        }

        public M_FalconeSolicitud consultarSolicitud(int id)
        {
            var solicitud = _context.M_FalconeSolicituds
            .FromSqlRaw("EXEC [dbo].[M-Falcone_ConsultarSolicitud] @p0", id)
            .AsEnumerable()
            .FirstOrDefault();

            return solicitud;
        }

        public List<M_FalconeSolicitud> ConsultarSolicitudes(int? idUsuario,DateTime? fechaIngreso, string? estado)
        {
            var solicitudes = _context.M_FalconeSolicituds
            .FromSqlRaw("EXEC [dbo].[M-Falcone_ConsultarSolicitudes] @p0, @p1, @p2", idUsuario, fechaIngreso, estado)
            .ToList();

            return solicitudes;
        }
        

     
        public ResponseDTO CrearSolicitud (SolicitudDTO solicitud)
            {
                var result = _context.Database.ExecuteSqlRaw(
                "EXEC [dbo].[M-Falcone_CrearSolicitud] @p0, @p1, @p2, @p3",
                solicitud.Tipo, solicitud.DescripcionSolicitud, solicitud.Justificativo, solicitud.UsuarioCreadorId);

            if (result > 0)
            {
                return new ResponseDTO
                {
                    CodError = "0000",
                    MensajeError = "Solicitud Ingresada Correctamente"
                };
            }
            else
            {
                return new ResponseDTO
                {
                    CodError = "1111",
                    MensajeError = "Error al ingresar la solicitud"
                };
            }
        }

        public ResponseDTO estadoSolicitud(int id, string estado)
        {
            var result = _context.Database.ExecuteSqlRaw("EXEC [dbo].[M-Falcone_ActualizarEstadoSolicitud] @p0, @p1", id, estado);

            if (result == 0)
            {
                return new ResponseDTO
                {
                    CodError = "0000",
                    MensajeError = "Estado de la solicitud actualizado correctamente"
                };
            }
            else
            {
                return new ResponseDTO
                {
                    CodError = "1111",
                    MensajeError = "Solicitud no encontrada"
                };
            }
        }
    }
}
