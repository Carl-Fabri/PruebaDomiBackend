using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaDomiruth.Core.Interfaces;

namespace PruebaTecnicaDomiruth.Core.Repository
{
    public class FuncionRepository : IRepository<Funcion>
    {
        private readonly CineDomiruthContext _context;
        public FuncionRepository(CineDomiruthContext context)
        {
            _context = context;
        }

        public async Task<List<Funcion>> GetAllAsync()
        {
            return await _context.Funciones
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .ToListAsync();
        }

        public async Task<Funcion> GetByIdAsync(int id)
        {
            return await _context.Funciones
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AddAsync(Funcion funcion)
        {
            await _context.Funciones.AddAsync(funcion);
        }

        public void Update(Funcion funcion)
        {
            _context.Funciones.Update(funcion);
        }

        public void Delete(Funcion funcion)
        {
            _context.Funciones.Remove(funcion);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Funcion>> GetFuncionesDisponiblesAsync(int? peliculaId = null, DateTime? fecha = null)
        {
            var query = _context.Funciones
                .Where(f => f.EntradasDisponibles > 0)
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .AsQueryable();

            if (peliculaId.HasValue)
                query = query.Where(f => f.PeliculaId == peliculaId.Value);

            if (fecha.HasValue)
                query = query.Where(f => f.HoraInicio.Date == fecha.Value.Date);

            return await query.OrderBy(f => f.HoraInicio).ToListAsync();
        }
    }

}
