using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsuarios.Models.Message
{
    public class MessageResponse
    {
        public string message { get; set; } = string.Empty;
        public bool? success { get; set; }
    }
}
