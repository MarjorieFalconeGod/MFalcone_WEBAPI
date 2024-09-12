using MFalcone_WEBAPI.Models;
using MFalcone_WEBAPI.Models.DTO;

namespace MFalcone_WEBAPI.Services.SolicitudServices
{
    public interface ISolicitudService
    {

        List<M_FalconeSolicitud> ConsultarSolicitudes(int? idUsuario , DateTime? fechaIngreso , string? estado);

        ResponseDTO CrearSolicitud(SolicitudDTO solicitud);

        ResponseDTO ActualizarSolicitud(int id, SolicitudDTO solicitud);

        ResponseDTO estadoSolicitud(int id , string estado);
        
        M_FalconeSolicitud consultarSolicitud(int id);



    }
}
