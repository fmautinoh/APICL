using APICL.Modelos;
using APICL.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace APICL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly ILogger<InventarioController> _logger;
        private readonly DbBolivarContext _dbBolivarContext;
        public InventarioController(ILogger<InventarioController> logger, DbBolivarContext db)
        {

            _logger = logger;
            _dbBolivarContext = db;

        }

        [HttpGet]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found

        public ActionResult<IEnumerable<InventarioDto>> GetInv()
        {
            _logger.LogInformation("Obtener Inventario de Productos");
            return Ok(_dbBolivarContext.Inventarios.ToListAsync().Result);
        }


        [HttpGet("{idInv:int}", Name = "GetInventarioId")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public ActionResult<InventarioDto> GetInvId(int idinv)
        {
            if (idinv == 0)
            {
                _logger.LogError("Error de Id Producto" + idinv);
                return BadRequest();
            }
            var inv = _dbBolivarContext.Inventarios.FirstOrDefault(c => c.IdInv == idinv);
            if (inv == null)
            {
                return NotFound();
            }
            return Ok(inv);
        }

        [HttpPost]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        public ActionResult<InventarioDto> CrearProducto([FromBody] InventarioDto inv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (inv == null)
            {
                return BadRequest(inv);
            }
            if (inv.IdProd == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            int estatus = 0;
            if (inv.StokPro>0)
            {
                 estatus = 1;
            }
            var prod = _dbBolivarContext.Productos.FirstOrDefault(c => c.IdProd == inv.IdProd);
            if (prod == null)
            {
                return NotFound();
            }
            Inventario prodmd = new()
            {
                IdProd = inv.IdProd,
                StokPro = inv.StokPro,
                IdPresent = inv.IdPresent,
                EstadoInv = estatus,
                FechProduc = DateTime.Now
            };
            _dbBolivarContext.Inventarios.Add(prodmd);
            _dbBolivarContext.SaveChanges();
            return Ok(prodmd);
        }

        [HttpPut("{idInv:int}")]
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(200)]//No content
        public IActionResult UpdateInventario(int idInv, [FromBody] InventarioDto inv)
        {
            if (inv == null || idInv != inv.IdInv)
            {
                return BadRequest();
            }
            int estatus = 0;
            if (inv.StokPro > 0)
            {
                estatus = 1;
            }
            Inventario invup = new()
            {
                IdInv=inv.IdInv,
                IdProd = inv.IdProd,
                StokPro = inv.StokPro,
                IdPresent = inv.IdPresent,
                EstadoInv = estatus,
                FechProduc = DateTime.Now
            };
            _dbBolivarContext.Inventarios.Update(invup);
            _dbBolivarContext.SaveChanges();
            return Ok(invup);
        }

    }
}
