using APICL.Modelos;
using APICL.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProductoController : ControllerBase

    {
        private readonly ILogger<ClienteController> _logger;
        private readonly DbBolivarContext _dbBolivarContext;
        public TipoProductoController(ILogger<ClienteController> logger, DbBolivarContext db)
        {

            _logger = logger;
            _dbBolivarContext = db;

        }
        [HttpGet]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public ActionResult<IEnumerable<TipoProductoDto>> GetTpProd()
        {
            _logger.LogInformation("Obtener Tipos productos");
            return Ok(_dbBolivarContext.TipoProductos.ToListAsync().Result);
        }
    }
}
