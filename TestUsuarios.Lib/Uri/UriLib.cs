using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsuarios.Models.Pagination;

namespace TestUsuarios.Lib.Uri
{
    public class UriLib: IUriLib
    {
        private readonly String _baseUri;
        public UriLib(String baseUri)
        {
            _baseUri = baseUri;
        }
        public System.Uri GetPageUri(Paginator paginator, string route)
        {
            var _enpointUri = new System.Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", paginator.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", paginator.PageSize.ToString());
            return new System.Uri(modifiedUri);
        }
    }
}
