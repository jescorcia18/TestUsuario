using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TestUsuarios.Models.Usuarios;
using TesUsuarios.Data.Entities;

namespace TestUsuarios.Lib.Utils
{
    public class Utils: IUtils
    {
        #region Mappers Usuario
        public async Task<UsuariosDataModel> MapperUsuarioModelToEntity(UsuarioRequest model, bool isCreate)
        {
            UsuariosDataModel entity = new UsuariosDataModel
            {
                Name = model.Name,
                Email = model.Email,
                Age = model.Age
            };

            return await Task.FromResult(entity);
        }

        public async Task<List<UsuarioRead>> MapperUsuarioListEntitytoModel(List<UsuariosDataModel> entity)
        {
            List<UsuarioRead> model = new List<UsuarioRead>();
            foreach (var obj in entity)
            {
                model.Add(new UsuarioRead
                {
                    IdUsuario = obj.Id,
                    Name = obj.Name,
                    Email = obj.Email,
                    Age = obj.Age
                });
            }
            return await Task.FromResult(model);
        }

        #endregion

        #region Validations
        public async Task<bool> isValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return await Task.FromResult(true);
            }
            catch (FormatException)
            {
                return await Task.FromResult(false);
            }
        }
        public async Task<bool> isValidDatetime(string dateTime)
        {
            DateTime temp;
            if (DateTime.TryParse(dateTime, out temp))
                return await Task.FromResult(true);
            else
                return await Task.FromResult(false);
        }
        public async Task<bool> isValidDecimal(string valueDecimal)
        {
            decimal value;
            if (Decimal.TryParse(valueDecimal, out value))
                return await Task.FromResult(true);
            else
                return await Task.FromResult(false);

            //ToDo: verificar que tenga solo 2 decimales
        }
        #endregion
    }
}
