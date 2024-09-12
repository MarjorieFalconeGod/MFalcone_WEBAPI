using MFalcone_WEBAPI.Models;
using MFalcone_WEBAPI.Models.DTO;

namespace MFalcone_WEBAPI.Services.UsuarioServices
{
    public interface IUsuarioService
    {
        LoginResponse login(Login login);
        M_FalconeUsuario crearUsuario(UsuarioDTO usuario);



    }
}
