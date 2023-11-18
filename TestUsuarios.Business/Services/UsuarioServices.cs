using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsuarios.Business.IServices;
using TestUsuarios.Lib.Pagination;
using TestUsuarios.Lib.Uri;
using TestUsuarios.Lib.Utils;
using TestUsuarios.Models.Message;
using TestUsuarios.Models.Pagination;
using TestUsuarios.Models.Usuarios;
using TesUsuarios.Data.Repository.Usuarios;

namespace TestUsuarios.Business.Services
{
    public class UsuarioServices : IUsuariosServices
    {
        #region Global Variables
        private readonly IUsuarioRepository _repoUsuario;
        private readonly IUtils _utils;
        private readonly IUriLib _uriService;
        #endregion

        #region Constructor Method
        public UsuarioServices(IUsuarioRepository repoUsuario, IUtils utils, IUriLib uriServices)
        {
            _repoUsuario = repoUsuario;
            _utils = utils;
            _uriService = uriServices;
        }
        #endregion

        #region Public Methods
        public async Task<UsuarioResponse> CreateUsuarioService(UsuarioRequest request)
        {
            try
            {
                if (!await ValidateFields(request))
                {
                    return new UsuarioResponse
                    {
                        IdUsuario = null,
                        MessageResponse = new MessageResponse
                        {
                            message = "One or more fields do not have the allowed value",
                            success = false
                        }
                    };
                }

                int idExist = await IsExistCandidate(request, _uriService, string.Empty);

                if (idExist > 0)
                {
                    return new UsuarioResponse
                    {
                        IdUsuario = idExist,
                        MessageResponse = new MessageResponse
                        {
                            message = "User is already registered with this email.",
                            success = false
                        }
                    };

                }

                var entity = await _utils.MapperUsuarioModelToEntity(request, true);

                var objResult = await _repoUsuario.Create(entity);

                if (objResult != null)
                {
                    return new UsuarioResponse
                    {
                        IdUsuario = objResult.Id,
                        MessageResponse = new MessageResponse { message = "User successfully registered.", success = true }
                    };
                }
                else
                    throw new Exception("Could not create User.");
            }
            catch (Exception ex)
            {
                return new UsuarioResponse
                {
                    IdUsuario = null,
                    MessageResponse = new MessageResponse { message = ex.Message, success = false }
                };
            }
        }

        public async Task<Paged<UsuarioRead>> GetAllUsuariosService(UsuarioRequest searchRequest, Paginator paginator, Sorter sorter)
        {
            try
            {
                return await Search(searchRequest, paginator, sorter, _uriService, false, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in the user GetAll service:" + ex.Message, ex);
            }
        }

        #endregion

        #region Private Methods
        private async Task<Paged<UsuarioRead>> Search(UsuarioRequest searchRequest, Paginator paginator, Sorter sorter, IUriLib uriservice, bool validateEmail, string route)
        {
            try
            {
                int totalRecords;
                if (string.IsNullOrWhiteSpace(sorter.SortBy) || sorter.SortBy == "id")
                {
                    sorter.SortBy = "email";
                }
                var entities = await _repoUsuario.Search(searchRequest, paginator, sorter, validateEmail);

                var usuarioRead = await _utils.MapperUsuarioListEntitytoModel(entities);

                if (validateEmail)
                    totalRecords = entities.Count;
                else
                    totalRecords = await _repoUsuario.TotalCount(searchRequest);

                var page = PaginationHelper.CreatePagedReponse<UsuarioRead>(usuarioRead, paginator, totalRecords, uriservice, route);
                return page;

            }
            catch (Exception ex)
            {
                throw new Exception("Error in the user search service:" + ex.Message, ex);
            }
        }
        private async Task<bool> ValidateFields(UsuarioRequest obj)
        {
            bool result = true;
            if (string.IsNullOrEmpty(obj.Name)) result = false;
            if (obj.Age < 0) result = false;
            if (!await _utils.isValidEmail(obj.Email)) result = false;

            return await Task.FromResult(result);
        }
        private async Task<int> IsExistCandidate(UsuarioRequest request, IUriLib uriservice, string route)
        {
            try
            {
                Paginator paginator = new() { PageNumber = 1, PageSize = 1 };

                Sorter sorter = new() { SortBy = "email", SortOrder = "asc" };

                var usuarioRead = await Search(request, paginator, sorter, uriservice, true, route);

                //Exist usuario
                if (usuarioRead.Items != null && usuarioRead.Items.Count > 0 && usuarioRead.Items.First().Email.Equals(request.Email.ToUpper()))
                    return usuarioRead.Items.First().IdUsuario;
                else //Not Exist usuario
                    return 0;

            }
            catch (Exception ex)
            {
                throw new Exception("Error in the user search service:" + ex.Message, ex);
            }
        }
        #endregion
    }

}