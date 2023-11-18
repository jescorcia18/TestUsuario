using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsuarios.Models.Message;

namespace TestUsuarios.Models.Usuarios
{
    public class UsuarioResponse
    {
        public int? IdUsuario{ get; set; }
        public MessageResponse MessageResponse { get; set; } = new MessageResponse();
    }
}
