using Microsoft.AspNetCore.Mvc;
using MFalcone_WEBAPI.Models.DTO;
using MFalcone_WEBAPI.Services.SolicitudServices;


namespace MFalcone_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly ISolicitudService _service;


        public SolicitudController(ISolicitudService service)
        {
            _service = service;
        }


        [HttpGet("consultarSolicitudes")]
        public IActionResult ConsultarSolicitudes([FromQuery] int? idUsuario, [FromQuery] DateTime? fechaIngreso, [FromQuery] string? estado)
        {
            return Ok(_service.ConsultarSolicitudes(idUsuario, fechaIngreso, estado));
        }

        [HttpPost("crearSolicitud")]
        public IActionResult CrearSolicitud([FromBody] SolicitudDTO solicitud)
        {
            if (solicitud == null)
            {
                return BadRequest();
            }

            return Ok(_service.CrearSolicitud(solicitud));

        }

        [HttpPut("actualizarSolicitud/{id}")]
        public IActionResult ActualizarSolicitud(int id, [FromBody] SolicitudDTO solicitud)
        {
            if (solicitud == null)
            {
                return BadRequest();
            }

            return Ok(_service.ActualizarSolicitud(id, solicitud));

        }

        [HttpPut("estadoSolicitud/{id}")]
        public IActionResult estadoSolicitud(int id, [FromQuery] string estado)
        {
            return Ok(_service.estadoSolicitud(id, estado));
        }


        [HttpGet("consultarSolicitud/{id}")]
        public IActionResult consultarSolicitud([FromRoute] int id)
        {
            return Ok(_service.consultarSolicitud(id));
        }




    }
}
