using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaDomiruth.Core.Services;
using PruebaTecnicaDomiruth.DTOs;

namespace PruebaTecnicaDomiruth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ReservaService _reservaService;

        public ReservasController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearReserva([FromBody] ReservaRequest request)
        {
            try
            {
                var response = await _reservaService.CrearReservaAsync(request);
                return CreatedAtAction(nameof(ObtenerReserva), new { id = response.Id }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerReserva(int id)
        {
            var reserva = await _reservaService.ObtenerReservaAsync(id);
            if (reserva == null) return NotFound();
            return Ok(reserva);
        }

        [HttpGet("cliente/{numeroDocumento}")]
        public async Task<IActionResult> ObtenerReservasPorCliente(string numeroDocumento)
        {
            var reservas = await _reservaService.ObtenerReservasPorClienteAsync(numeroDocumento);
            return Ok(reservas);
        }
    }
}
