using DB.Models;
using PruebaTecnicaDomiruth.Core.Repository;
using PruebaTecnicaDomiruth.DTOs;

namespace PruebaTecnicaDomiruth.Core.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteService(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteDto> CreateClienteAsync(ClienteCreateDto clienteDto)
        {
            var clienteExistente = await _clienteRepository.GetByDocumentoAsync(clienteDto.NumeroDocumento);
            if (clienteExistente != null)
                throw new InvalidOperationException("El cliente ya existe");

            var cliente = new Cliente
            {
                Nombres = clienteDto.Nombres,
                Apellidos = clienteDto.Apellidos,
                FechaNacimiento = clienteDto.FechaNacimiento,
                Genero = clienteDto.Genero,
                TipoDocumento = clienteDto.TipoDocumento,
                NumeroDocumento = clienteDto.NumeroDocumento,
                Email = clienteDto.Email
            };

            await _clienteRepository.AddAsync(cliente);
            await _clienteRepository.SaveAsync();

            return new ClienteDto
            {
                Id = cliente.Id,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                NumeroDocumento = cliente.NumeroDocumento,
                Email = cliente.Email
            };
        }

        public async Task<ClienteDto> GetClienteByDocumentoAsync(string numeroDocumento)
        {
            var cliente = await _clienteRepository.GetByDocumentoAsync(numeroDocumento);
            if (cliente == null) return null;

            return new ClienteDto
            {
                Id = cliente.Id,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                NumeroDocumento = cliente.NumeroDocumento,
                Email = cliente.Email
            };
        }

        public async Task<List<ClienteDto>> GetAllClientesAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();

            var result = new List<ClienteDto>();

            foreach (var cliente in clientes)
            {
                result.Add(new ClienteDto
                {
                    Id = cliente.Id,
                    Nombres = cliente.Nombres,
                    Apellidos = cliente.Apellidos,
                    NumeroDocumento = cliente.NumeroDocumento,
                    Email = cliente.Email
                });
            }

            return result;
        }
    }
}
