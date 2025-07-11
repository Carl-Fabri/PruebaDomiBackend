using DB.Models;
using PruebaTecnicaDomiruth.Core.Repository;
using PruebaTecnicaDomiruth.DTOs;

namespace PruebaTecnicaDomiruth.Core.Services
{
    public class FuncionService
    {
        private readonly FuncionRepository _funcionRepository;
        private readonly PeliculaRepository _peliculaRepository;

        public FuncionService(FuncionRepository funcionRepository, PeliculaRepository peliculaRepository)
        {
            _funcionRepository = funcionRepository;
            _peliculaRepository = peliculaRepository;
        }

        public async Task<List<FuncionDto>> GetFuncionesDisponiblesAsync(int? peliculaId = null, DateTime? fecha = null)
        {
            var funciones = await _funcionRepository.GetFuncionesDisponiblesAsync(peliculaId, fecha);

            var result = new List<FuncionDto>();

            foreach (var funcion in funciones)
            {
                result.Add(new FuncionDto
                {
                    Id = funcion.Id,
                    Pelicula = funcion.Pelicula.Titulo,
                    Sala = funcion.Sala.Nombre,
                    HoraInicio = funcion.HoraInicio,
                    HoraFin = funcion.HoraInicio.AddMinutes(funcion.Pelicula.DuracionMinutos),
                    EntradasDisponibles = funcion.EntradasDisponibles,
                    DuracionPelicula = funcion.Pelicula.DuracionMinutos
                });
            }

            return result;
        }

        public async Task<FuncionDto> CreateFuncionAsync(FuncionCreateDto funcionDto)
        {
            var pelicula = await _peliculaRepository.GetByIdAsync(funcionDto.PeliculaId);
            if (pelicula == null)
                throw new KeyNotFoundException("La película no existe");

            var funcion = new Funcion
            {
                PeliculaId = funcionDto.PeliculaId,
                SalaId = funcionDto.SalaId,
                HoraInicio = funcionDto.HoraInicio,
                EntradasDisponibles = funcionDto.EntradasDisponibles
            };

            await _funcionRepository.AddAsync(funcion);
            await _funcionRepository.SaveAsync();

            return new FuncionDto
            {
                Id = funcion.Id,
                Pelicula = pelicula.Titulo,
                Sala = funcion.Sala?.Nombre ?? "No asignada",
                HoraInicio = funcion.HoraInicio,
                HoraFin = funcion.HoraInicio.AddMinutes(pelicula.DuracionMinutos),
                EntradasDisponibles = funcion.EntradasDisponibles,
                DuracionPelicula = pelicula.DuracionMinutos
            };
        }

        public async Task<FuncionDto> GetFuncionByIdAsync(int id)
        {
            var funcion = await _funcionRepository.GetByIdAsync(id);
            if (funcion == null) return null;

            return new FuncionDto
            {
                Id = funcion.Id,
                Pelicula = funcion.Pelicula.Titulo,
                Sala = funcion.Sala.Nombre,
                HoraInicio = funcion.HoraInicio,
                HoraFin = funcion.HoraInicio.AddMinutes(funcion.Pelicula.DuracionMinutos),
                EntradasDisponibles = funcion.EntradasDisponibles,
                DuracionPelicula = funcion.Pelicula.DuracionMinutos
            };
        }
    }
}
