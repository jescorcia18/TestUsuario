using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsuarios.Lib.Uri;
using TestUsuarios.Models.Pagination;
using TestUsuarios.Models.Usuarios;

namespace TestUsuarios.Business.IServices
{
    public interface IUsuariosServices
    {
        Task<UsuarioResponse> CreateUsuarioService(UsuarioRequest request);
        Task<Paged<UsuarioRead>> GetAllUsuariosService(UsuarioRequest searchRequest, Paginator paginator, Sorter sorter);
    }
}
