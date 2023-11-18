using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsuarios.Models.Pagination;
using TestUsuarios.Models.Usuarios;
using TesUsuarios.Data.Entities;

namespace TesUsuarios.Data.Repository.Usuarios
{
    public interface IUsuarioRepository
    {
        Task<UsuariosDataModel> Create(UsuariosDataModel request);
        Task<List<UsuariosDataModel>> Search(UsuarioRequest searchRequest, Paginator paginator, Sorter sorter, bool validateEmail = false);
        Task<int> TotalCount(UsuarioRequest searchRequest);
        Task<UsuariosDataModel?> Read(int id);
        Task<UsuariosDataModel> Update(int id, UsuariosDataModel entity);
        Task Delete(int id);
    }
}
