using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaDomiruth.Core.Interfaces;

namespace PruebaTecnicaDomiruth.Core.Repository
{
    public class PeliculaRepository : IRepository<Pelicula>
    {

        private readonly CineDomiruthContext _context;

        public PeliculaRepository(CineDomiruthContext context)
        {
            _context = context;
        }

        // Métodos CRUD básicos
        public async Task<List<Pelicula>> GetAllAsync()
        {
            return await _context.Peliculas.ToListAsync();
        }

        public async Task<Pelicula> GetByIdAsync(int id)
        {
            return await _context.Peliculas.FindAsync(id);
        }

        public async Task AddAsync(Pelicula pelicula)
        {
            await _context.Peliculas.AddAsync(pelicula);
        }

        public void Update(Pelicula pelicula)
        {
            _context.Peliculas.Update(pelicula);
        }

        public void Delete(Pelicula pelicula)
        {
            _context.Peliculas.Remove(pelicula);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Pelicula>> GetCarteleraAsync(DateTime? fecha = null, int? generoId = null, string? titulo = null)
        {
            var query = _context.Peliculas
                .Include(p => p.Genero)
                .Include(p => p.Funciones)
                    .ThenInclude(f => f.Sala)
                .AsQueryable();

            if (generoId.HasValue)
                query = query.Where(p => p.GeneroId == generoId.Value);

            if (fecha.HasValue)
                query = query.Where(p => p.Funciones.Any(f => f.HoraInicio.Date == fecha.Value.Date));

            if (!string.IsNullOrEmpty(titulo))
                query = query.Where(p => p.Titulo.Contains(titulo));

            return await query.ToListAsync();
        }

        public async Task<Pelicula> GetPeliculaConFuncionesAsync(int id)
        {
            return await _context.Peliculas
                .Include(p => p.Genero)
                .Include(p => p.Funciones)
                    .ThenInclude(f => f.Sala)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pelicula>> GetPeliculasPorGeneroAsync(int generoId)
        {
            return await _context.Peliculas
                .Where(p => p.GeneroId == generoId)
                .ToListAsync();
        }

        public async Task<List<Funcion>> GetFuncionesDisponiblesAsync(int peliculaId, DateTime? fecha = null)
        {
            var query = _context.Funciones
                .Where(f => f.PeliculaId == peliculaId && f.EntradasDisponibles > 0);

            if (fecha.HasValue)
                query = query.Where(f => f.HoraInicio.Date == fecha.Value.Date);

            return await query
                .Include(f => f.Sala)
                .OrderBy(f => f.HoraInicio)
                .ToListAsync();
        }
    }
}