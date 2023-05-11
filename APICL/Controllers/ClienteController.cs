using APICL.Modelos;
using APICL.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly DbBolivarContext _dbBolivarContext;
        public ClienteController(ILogger<ClienteController> logger, DbBolivarContext db)
        {

            _logger = logger;
            _dbBolivarContext = db;

        }
        //-----------
        [HttpGet]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found

        public ActionResult<IEnumerable<ClienteDto>> GetClientes()
        {
            _logger.LogInformation("Obtener Clientes");
            return Ok(_dbBolivarContext.Clientes.ToListAsync().Result);
        }
        //-----------
        [HttpGet("{idcliente:int}", Name ="GetClienteId")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public ActionResult<ClienteDto> GetClientesId(int idcliente)
        {
            if (idcliente == 0)
            {
                _logger.LogError("Error de Id cliente"+idcliente);
                return BadRequest();
            }
            var client = _dbBolivarContext.Clientes.FirstOrDefault(c => c.IdCli == idcliente);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }
        //-----------
        [HttpPost]
        [ProducesResponseType(201)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        public ActionResult<ClienteDto> CrearCliente([FromBody]ClienteDto cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(cliente == null)
            {
                return BadRequest(cliente);
            }
            if(cliente.id_cli == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Cliente clientemd = new()
            {
                NomCli = cliente.nom_cli,
                ApatCli = cliente.apat_cli,
                AmatCli = cliente.amat_cli,
                TelCli = cliente.tel_cli,
                CorCli = cliente.cor_cli,
                DirCli = cliente.dir_cli,
                DniCli = cliente.dni_cli,
                RucCli = cliente.ruc_cli
            };
            _dbBolivarContext.Clientes.Add(clientemd);
            _dbBolivarContext.SaveChanges();
            return Ok(clientemd);
        }

        [HttpPut("{idcliente:int}")]
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(200)]//No content
        public IActionResult updateCliente(int idcliente, [FromBody] ClienteDto clienteu)
        {
            if (clienteu == null || idcliente != clienteu.id_cli)
            {
                return BadRequest();
            }
            Cliente clientemd = new()
            {
                IdCli = clienteu.id_cli,
                NomCli = clienteu.nom_cli,
                ApatCli = clienteu.apat_cli,
                AmatCli = clienteu.amat_cli,
                TelCli = clienteu.tel_cli,
                CorCli = clienteu.cor_cli,
                DirCli = clienteu.dir_cli,
                DniCli = clienteu.dni_cli,
                RucCli = clienteu.ruc_cli
            };
            _dbBolivarContext.Clientes.Update(clientemd);
            _dbBolivarContext.SaveChanges();
            return Ok(clientemd);
        }

        [HttpPatch("{idcliente:int}")]
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(204)]//No content
        public IActionResult updatePartialCliente(int idcliente, JsonPatchDocument<ClienteDto> patchcliente)
        {
            if (patchcliente == null || idcliente == 0)
            {
                return BadRequest();
            }
            var clientec = _dbBolivarContext.Clientes.FirstOrDefault(c => c.IdCli == idcliente);
            ClienteDto clientemd = new()
            {
                id_cli = clientec.IdCli,
                nom_cli = clientec.NomCli,
                apat_cli = clientec.ApatCli,
                amat_cli = clientec.AmatCli,
                tel_cli = clientec.TelCli,
                cor_cli = clientec.CorCli,
                dir_cli = clientec.DirCli,
                dni_cli = clientec.DniCli,
                ruc_cli = clientec.RucCli
            };
            if (clientec == null) { return BadRequest(); }
            patchcliente.ApplyTo(clientemd, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cliente clienteu = new()
            {
                IdCli = clientemd.id_cli,
                NomCli = clientemd.nom_cli,
                ApatCli = clientemd.apat_cli,
                AmatCli = clientemd.amat_cli,
                TelCli = clientemd.tel_cli,
                CorCli = clientemd.cor_cli,
                DirCli = clientemd.dir_cli,
                DniCli = clientemd.dni_cli,
                RucCli = clientemd.ruc_cli
            };

            _dbBolivarContext.Clientes.Update(clienteu);
            _dbBolivarContext.SaveChanges();

            return NoContent();
        }
    }
}
