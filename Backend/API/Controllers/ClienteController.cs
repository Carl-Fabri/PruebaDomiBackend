using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaDomiruth.Core.Services;
using PruebaTecnicaDomiruth.DTOs;

namespace PruebaTecnicaDomiruth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientes()
        {
            var clientes = await _clienteService.GetAllClientesAsync();
            return Ok(clientes);
        }

        [HttpGet("{numeroDocumento}")]
        public async Task<IActionResult> GetClienteByDocumento(string numeroDocumento)
        {
            var cliente = await _clienteService.GetClienteByDocumentoAsync(numeroDocumento);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] ClienteCreateDto clienteDto)
        {
            try
            {
                var cliente = await _clienteService.CreateClienteAsync(clienteDto);
                return CreatedAtAction(nameof(GetClienteByDocumento),
                    new { numeroDocumento = cliente.NumeroDocumento }, cliente);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
