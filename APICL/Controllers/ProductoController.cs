using APICL.Modelos;
using APICL.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly DbBolivarContext _dbBolivarContext;
        public ProductoController(ILogger<ClienteController> logger, DbBolivarContext db)
        {

            _logger = logger;
            _dbBolivarContext = db;

        }

        [HttpGet]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found

        public ActionResult<IEnumerable<ProductoDto>> GetProd()
        {
            _logger.LogInformation("Obtener productos");
            return Ok(_dbBolivarContext.Productos.ToListAsync().Result);
        }

        [HttpGet("{idprod:int}", Name = "GetProductoId")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public ActionResult<ClienteDto> GetProdId(int idprod)
        {
            if (idprod == 0)
            {
                _logger.LogError("Error de Id Producto" + idprod);
                return BadRequest();
            }
            var prod = _dbBolivarContext.Productos.FirstOrDefault(c => c.IdProd == idprod);
            if (prod == null)
            {
                return NotFound();
            }
            return Ok(prod);
        }

        [HttpPost]
        [ProducesResponseType(201)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        public ActionResult<ProductoDto> CrearProducto([FromBody] ProductoDto prod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (prod == null)
            {
                return BadRequest(prod);
            }
            if (prod.IdProd == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Producto prodmd = new()
            {
                NomProd = prod.NomProd,
                IdTipPro = prod.IdTipPro,
                PrecioProd = prod.PrecioProd,
                DurProd = prod.DurProd
            };
            _dbBolivarContext.Productos.Add(prodmd);
            _dbBolivarContext.SaveChanges();
            return Ok(prodmd);
        }

        [HttpPut("{idprod:int}")]
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(200)]//No content
        public IActionResult updateProd(int idprod, [FromBody] ProductoDto prod)
        {
            if (prod == null || idprod != prod.IdProd)
            {
                return BadRequest();
            }
            Producto prodmd = new()
            {
                NomProd = prod.NomProd,
                IdTipPro = prod.IdTipPro,
                PrecioProd = prod.PrecioProd,
                DurProd = prod.DurProd
            };
            _dbBolivarContext.Productos.Update(prodmd);
            _dbBolivarContext.SaveChanges();
            return Ok(prodmd);
        }

        [HttpDelete("{idprod:int}")]
        [ProducesResponseType(200)]//No content
        [ProducesResponseType(204)]//a
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public IActionResult deleteProducto(int idprod)
        {
            if (idprod == 0)
            {
                return BadRequest();
            }
            try {
                var prods = _dbBolivarContext.Productos.FirstOrDefault(c => c.IdProd == idprod);
                if (prods == null)
                {
                    return NotFound();
                }
                _dbBolivarContext.Productos.Remove(prods);
                _dbBolivarContext.SaveChanges();
                return NoContent();
            }
            catch (Exception ex) { 
             return BadRequest(ex.Message);
            }
            
           
        }

        

    }
}
