using MFalcone_WEBAPI.Models;
using MFalcone_WEBAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace MFalcone_WEBAPI.Services.UsuarioServices
{
    public class UsuarioService : IUsuarioService
    {
        private M_Falcone_BDContext _context;

        public UsuarioService(M_Falcone_BDContext context)
        {
            _context = context;
        }

        public M_FalconeUsuario crearUsuario(UsuarioDTO usuario)
        {
            var result = _context.M_FalconeUsuarios
          .FromSqlRaw("EXEC [dbo].[M-Falcone_CrearUsuario] @p0, @p1, @p2, @p3, @p4, @p5",
              usuario.Username, usuario.Nombres, usuario.Email, "A", usuario.Password, usuario.Rol)
          .AsEnumerable()
          .FirstOrDefault();

            return result;

        }

        public LoginResponse login(Login login)
        {
            var usuario = _context.M_FalconeUsuarios
            .FromSqlRaw("EXEC [dbo].[M-Falcone_Login] @p0, @p1", login.Usuario, login.Clave)
            .AsEnumerable()
            .FirstOrDefault();

            if (usuario != null)
            {
                return new LoginResponse
                {
                    IsValid = true,
                    Rol = usuario.Rol
                };
            }
            else
            {
                return new LoginResponse
                {
                    IsValid = false,
                    Rol = null
                };
            }
        }
    }
}
