using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaDomiruth.Core.Interfaces;

namespace PruebaTecnicaDomiruth.Core.Repository
{
    public class ClienteRepository : IRepository<Cliente>
    {
        private readonly CineDomiruthContext _context;

        public ClienteRepository(CineDomiruthContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente> GetByDocumentoAsync(string numeroDocumento)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.NumeroDocumento == numeroDocumento);
        }

        public async Task AddAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
        }

        public void Update(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
        }

        public void Delete(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
