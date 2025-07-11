using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaDomiruth.Core.Interfaces;

namespace PruebaTecnicaDomiruth.Core.Repository
{
    public class ReservaRepository : IRepository<Reserva>
    {

        private readonly CineDomiruthContext _context;

        public ReservaRepository(CineDomiruthContext context)
        {
            _context = context;
        }
        // Métodos CRUD básicos
        public async Task<List<Reserva>> GetAllAsync()
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Funcion)
                    .ThenInclude(f => f.Pelicula)
                .Include(r => r.Funcion)
                    .ThenInclude(f => f.Sala)
                .ToListAsync();
        }

        public async Task<Reserva> GetByIdAsync(int id)
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Funcion)
                    .ThenInclude(f => f.Pelicula)
                .Include(r => r.Funcion)
                    .ThenInclude(f => f.Sala)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Reserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
        }

        public void Update(Reserva reserva)
        {
            _context.Reservas.Update(reserva);
        }

        public void Delete(Reserva reserva)
        {
            _context.Reservas.Remove(reserva);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Reserva> CrearReservaCompletaAsync(Cliente cliente, Funcion funcion, int cantidadEntradas)
        {
       
            if (funcion.EntradasDisponibles < cantidadEntradas)
            {
                throw new InvalidOperationException("No hay suficientes entradas disponibles");
            }

     
            var reserva = new Reserva
            {
                Cliente = cliente,
                Funcion = funcion,
                FechaReserva = DateTime.UtcNow,
                NumeroTicket = GenerarNumeroTicket(funcion),
                CantidadEntradas = cantidadEntradas
            };

            funcion.EntradasDisponibles -= cantidadEntradas;

            await _context.Reservas.AddAsync(reserva);
            _context.Funciones.Update(funcion);

            return reserva;
        }

        public async Task<List<Reserva>> GetReservasPorFuncionAsync(int funcionId)
        {
            return await _context.Reservas
                .Where(r => r.FuncionId == funcionId)
                .Include(r => r.Cliente)
                .OrderBy(r => r.FechaReserva)
                .ToListAsync();
        }

        public async Task<List<Reserva>> GetReservasPorClienteAsync(string numeroDocumento)
        {
            return await _context.Reservas
                .Where(r => r.Cliente.NumeroDocumento == numeroDocumento)
                .Include(r => r.Funcion)
                    .ThenInclude(f => f.Pelicula)
                .Include(r => r.Funcion)
                    .ThenInclude(f => f.Sala)
                .OrderByDescending(r => r.FechaReserva)
                .ToListAsync();
        }

        private string GenerarNumeroTicket(Funcion funcion)
        {
            var fecha = DateTime.Now.ToString("yyyyMMdd");
            var random = new Random().Next(1000, 9999).ToString();
            return $"CINE-{fecha}-{funcion.Id}-{random}";
        }
    }
}
