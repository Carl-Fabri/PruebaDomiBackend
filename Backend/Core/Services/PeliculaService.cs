using PruebaTecnicaDomiruth.Core.Repository;
using PruebaTecnicaDomiruth.DTOs;
using DB.Models;

namespace PruebaTecnicaDomiruth.Core.Services
{
    public class PeliculaService
    {
        private readonly PeliculaRepository _peliculaRepository;

        public PeliculaService(PeliculaRepository peliculaRepository)
        {
            _peliculaRepository = peliculaRepository;
        }
        public async Task<List<PeliculaDto>> GetCarteleraAsync(CarteleraFilterDto filtro)
        {
            var peliculas = await _peliculaRepository.GetCarteleraAsync(
                filtro.Fecha,
                filtro.GeneroId,
                filtro.Titulo);

            return peliculas.Select(p => new PeliculaDto
            {
                Id = p.Id,
                Titulo = p.Titulo,
                Genero = p.Genero.Nombre,
                Sinopsis = p.Sinopsis,
                DuracionMinutos = p.DuracionMinutos,
                ImagenUrl = p.ImagenUrl
            }).ToList();
        }

        public async Task<PeliculaDetailDto> GetPeliculaDetailAsync(int id)
        {
            var pelicula = await _peliculaRepository.GetPeliculaConFuncionesAsync(id);
            if (pelicula == null) return null;

            return new PeliculaDetailDto
            {
                Id = pelicula.Id,
                Titulo = pelicula.Titulo,
                Genero = pelicula.Genero.Nombre,
                Sinopsis = pelicula.Sinopsis,
                DuracionMinutos = pelicula.DuracionMinutos,
                ImagenUrl = pelicula.ImagenUrl,
                Funciones = pelicula.Funciones.Select(f => new FuncionDto
                {
                    Id = f.Id,
                    Sala = f.Sala.Nombre,
                    HoraInicio = f.HoraInicio,
                    HoraFin = f.HoraInicio.AddMinutes(pelicula.DuracionMinutos),
                    EntradasDisponibles = f.EntradasDisponibles
                }).OrderBy(f => f.HoraInicio).ToList()
            };
        }

        public async Task<List<FuncionDto>> GetFuncionesDisponiblesAsync(int peliculaId, DateTime? fecha = null)
        {
            var funciones = await _peliculaRepository.GetFuncionesDisponiblesAsync(peliculaId, fecha);

            var pelicula = await _peliculaRepository.GetByIdAsync(peliculaId);
            if (pelicula == null) return new List<FuncionDto>();

            return funciones.Select(f => new FuncionDto
            {
                Id = f.Id,
                Sala = f.Sala.Nombre,
                HoraInicio = f.HoraInicio,
                HoraFin = f.HoraInicio.AddMinutes(pelicula.DuracionMinutos),
                EntradasDisponibles = f.EntradasDisponibles
            }).ToList();
        }

        public async Task<List<PeliculaDto>> GetPeliculasByGeneroAsync(int generoId)
        {
            var peliculas = await _peliculaRepository.GetPeliculasPorGeneroAsync(generoId);

            return peliculas.Select(p => new PeliculaDto
            {
                Id = p.Id,
                Titulo = p.Titulo,
                Genero = p.Genero.Nombre,
                Sinopsis = p.Sinopsis,
                DuracionMinutos = p.DuracionMinutos,
                ImagenUrl = p.ImagenUrl
            }).ToList();
        }

        public async Task<PeliculaDto> CreatePeliculaAsync(PeliculaCreateDto peliculaDto)
        {
            // Verificar que el género existe (esto se podría mejorar con un repositorio de géneros)
            var pelicula = new Pelicula
            {
                Titulo = peliculaDto.Titulo,
                GeneroId = peliculaDto.GeneroId,
                Sinopsis = peliculaDto.Sinopsis,
                DuracionMinutos = peliculaDto.DuracionMinutos,
                ImagenUrl = peliculaDto.ImagenUrl
            };

            await _peliculaRepository.AddAsync(pelicula);
            await _peliculaRepository.SaveAsync();

            // Obtener la película creada con el género incluido
            var peliculaCreada = await _peliculaRepository.GetPeliculaConFuncionesAsync(pelicula.Id);

            return new PeliculaDto
            {
                Id = peliculaCreada.Id,
                Titulo = peliculaCreada.Titulo,
                Genero = peliculaCreada.Genero?.Nombre ?? "Sin género",
                Sinopsis = peliculaCreada.Sinopsis,
                DuracionMinutos = peliculaCreada.DuracionMinutos,
                ImagenUrl = peliculaCreada.ImagenUrl
            };
        }
    }
}
