using Microsoft.AspNetCore.Mvc;
using TestUsuarios.Business.IServices;
using TestUsuarios.Models.Pagination;
using TestUsuarios.Models.Usuarios;

namespace TestUsuarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        #region Global Variables
        private readonly IUsuariosServices _usuarioServices;
        #endregion
        #region Constructor Method
        public UsuariosController(IUsuariosServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }
        #endregion
        #region Controllers
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] UsuarioRequest request)
        {
            try
            {
                return Ok(await _usuarioServices.CreateUsuarioService(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] UsuarioRequest searchRequest, [FromQuery] Paginator paginator, [FromQuery] Sorter sorter)
        {
            try
            {
                return Ok(await _usuarioServices.GetAllUsuariosService(searchRequest, paginator, sorter));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
