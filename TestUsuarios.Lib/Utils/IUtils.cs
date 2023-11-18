using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsuarios.Models.Usuarios;
using TesUsuarios.Data.Entities;

namespace TestUsuarios.Lib.Utils
{
    public interface IUtils
    {
        Task<UsuariosDataModel> MapperUsuarioModelToEntity(UsuarioRequest model, bool isCreate);
        Task<List<UsuarioRead>> MapperUsuarioListEntitytoModel(List<UsuariosDataModel> entity);

        Task<bool> isValidEmail(string email);
        Task<bool> isValidDatetime(string dateTime);
        Task<bool> isValidDecimal(string valueDecimal);
    }
}
