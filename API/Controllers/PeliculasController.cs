using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaDomiruth.Core.Services;
using PruebaTecnicaDomiruth.DTOs;

namespace PruebaTecnicaDomiruth.API.Controllers
{
    /// <summary>
    /// Controlador para la gestión de películas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly PeliculaService _peliculaService;

        public PeliculasController(PeliculaService peliculaService)
        {
            _peliculaService = peliculaService;
        }

        /// <summary>
        /// Crear una nueva película
        /// </summary>
        /// <param name="peliculaDto">Datos de la película a crear</param>
        /// <returns>La película creada</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePelicula([FromBody] PeliculaCreateDto peliculaDto)
        {
            try
            {
                var pelicula = await _peliculaService.CreatePeliculaAsync(peliculaDto);
                return CreatedAtAction(nameof(GetPeliculaDetail), new { id = pelicula.Id }, pelicula);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtener la cartelera de películas
        /// </summary>
        /// <param name="filtro">Filtros para la cartelera</param>
        /// <returns>Lista de películas filtradas</returns>
        [HttpGet]
        public async Task<IActionResult> GetCartelera([FromQuery] CarteleraFilterDto filtro)
        {
            var peliculas = await _peliculaService.GetCarteleraAsync(filtro);
            return Ok(peliculas);
        }

        /// <summary>
        /// Obtener detalle de una película por ID
        /// </summary>
        /// <param name="id">ID de la película</param>
        /// <returns>Detalle de la película</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeliculaDetail(int id)
        {
            var pelicula = await _peliculaService.GetPeliculaDetailAsync(id);
            if (pelicula == null) return NotFound();
            return Ok(pelicula);
        }

        /// <summary>
        /// Obtener funciones disponibles para una película
        /// </summary>
        /// <param name="id">ID de la película</param>
        /// <param name="fecha">Fecha opcional para filtrar funciones</param>
        /// <returns>Lista de funciones disponibles</returns>
        [HttpGet("{id}/funciones")]
        public async Task<IActionResult> GetFuncionesDisponibles(int id, [FromQuery] DateTime? fecha = null)
        {
            var funciones = await _peliculaService.GetFuncionesDisponiblesAsync(id, fecha);
            return Ok(funciones);
        }

        /// <summary>
        /// Obtener películas por género
        /// </summary>
        /// <param name="generoId">ID del género</param>
        /// <returns>Lista de películas del género especificado</returns>
        [HttpGet("genero/{generoId}")]
        public async Task<IActionResult> GetPeliculasByGenero(int generoId)
        {
            var peliculas = await _peliculaService.GetPeliculasByGeneroAsync(generoId);
            return Ok(peliculas);
        }
    }
}
