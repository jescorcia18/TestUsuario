using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsuarios.Models.Pagination;

namespace TestUsuarios.Lib.Uri
{
    public interface IUriLib
    {
        System.Uri GetPageUri(Paginator paginator, string route);
    }
}
