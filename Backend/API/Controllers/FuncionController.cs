using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaDomiruth.Core.Services;
using PruebaTecnicaDomiruth.DTOs;

namespace PruebaTecnicaDomiruth.API.Controllers
{
    /// <summary>
    /// Controlador para la gestión de funciones de cine
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionesController : ControllerBase
    {
        private readonly FuncionService _funcionService;

        public FuncionesController(FuncionService funcionService)
        {
            _funcionService = funcionService;
        }

        /// <summary>
        /// Crear una nueva función
        /// </summary>
        /// <param name="funcionDto">Datos de la función a crear</param>
        /// <returns>La función creada</returns>
        [HttpPost]
        public async Task<IActionResult> CreateFuncion([FromBody] FuncionCreateDto funcionDto)
        {
            try
            {
                var funcion = await _funcionService.CreateFuncionAsync(funcionDto);
                return CreatedAtAction(nameof(GetFuncionById), new { id = funcion.Id }, funcion);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtener funciones disponibles con filtros opcionales
        /// </summary>
        /// <param name="peliculaId">ID de la película (opcional)</param>
        /// <param name="fecha">Fecha específica (opcional)</param>
        /// <returns>Lista de funciones disponibles</returns>
        [HttpGet]
        public async Task<IActionResult> GetFuncionesDisponibles(
            [FromQuery] int? peliculaId = null,
            [FromQuery] DateTime? fecha = null)
        {
            var funciones = await _funcionService.GetFuncionesDisponiblesAsync(peliculaId, fecha);
            return Ok(funciones);
        }

        /// <summary>
        /// Obtener función por ID
        /// </summary>
        /// <param name="id">ID de la función</param>
        /// <returns>Detalle de la función</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFuncionById(int id)
        {
            var funcion = await _funcionService.GetFuncionByIdAsync(id);
            if (funcion == null) return NotFound();
            return Ok(funcion);
        }
    }
}
