using DB.Models;
using PruebaTecnicaDomiruth.Core.Repository;
using PruebaTecnicaDomiruth.DTOs;

namespace PruebaTecnicaDomiruth.Core.Services
{
    public class ReservaService
    {
        private readonly ReservaRepository _reservaRepository;
        private readonly FuncionRepository _funcionRepository;
        private readonly ClienteRepository _clienteRepository;

        public ReservaService(
            ReservaRepository reservaRepository,
            FuncionRepository funcionRepository,
            ClienteRepository clienteRepository)
        {
            _reservaRepository = reservaRepository;
            _funcionRepository = funcionRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<ReservaResponse> CrearReservaAsync(ReservaRequest request)
        {
            // Validación básica
            if (request.CantidadEntradas <= 0)
                throw new ArgumentException("La cantidad de entradas debe ser mayor a cero");

            // Obtener la función
            var funcion = await _funcionRepository.GetByIdAsync(request.FuncionId);
            if (funcion == null)
                throw new KeyNotFoundException("La función no existe");

            // Buscar o crear cliente
            var cliente = await _clienteRepository.GetByDocumentoAsync(request.NumeroDocumento) ??
                new Cliente
                {
                    Nombres = request.Nombres,
                    Apellidos = request.Apellidos,
                    FechaNacimiento = request.FechaNacimiento,
                    Genero = request.Genero,
                    TipoDocumento = request.TipoDocumento,
                    NumeroDocumento = request.NumeroDocumento,
                    Email = request.Email
                };

            // Crear reserva
            var reserva = await _reservaRepository.CrearReservaCompletaAsync(
                cliente,
                funcion,
                request.CantidadEntradas);

            await _reservaRepository.SaveAsync();

            return new ReservaResponse
            {
                Id = reserva.Id,
                NumeroTicket = reserva.NumeroTicket,
                Pelicula = funcion.Pelicula.Titulo,
                Sala = funcion.Sala.Nombre,
                HoraInicio = funcion.HoraInicio,
                HoraFin = funcion.HoraInicio.AddMinutes(funcion.Pelicula.DuracionMinutos),
                CantidadEntradas = reserva.CantidadEntradas,
                FechaReserva = reserva.FechaReserva,
                Cliente = new ClienteInfo
                {
                    Nombres = cliente.Nombres,
                    Apellidos = cliente.Apellidos,
                    NumeroDocumento = cliente.NumeroDocumento
                }
            };
        }

        public async Task<ReservaResponse> ObtenerReservaAsync(int id)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id);
            if (reserva == null) return null;

            return new ReservaResponse
            {
                Id = reserva.Id,
                NumeroTicket = reserva.NumeroTicket,
                Pelicula = reserva.Funcion.Pelicula.Titulo,
                Sala = reserva.Funcion.Sala.Nombre,
                HoraInicio = reserva.Funcion.HoraInicio,
                HoraFin = reserva.Funcion.HoraInicio.AddMinutes(reserva.Funcion.Pelicula.DuracionMinutos),
                CantidadEntradas = reserva.CantidadEntradas,
                FechaReserva = reserva.FechaReserva,
                Cliente = new ClienteInfo
                {
                    Nombres = reserva.Cliente.Nombres,
                    Apellidos = reserva.Cliente.Apellidos,
                    NumeroDocumento = reserva.Cliente.NumeroDocumento
                }
            };
        }

        public async Task<List<ReservaResponse>> ObtenerReservasPorClienteAsync(string numeroDocumento)
        {
            var reservas = await _reservaRepository.GetReservasPorClienteAsync(numeroDocumento);

            return reservas.ConvertAll(r => new ReservaResponse
            {
                Id = r.Id,
                NumeroTicket = r.NumeroTicket,
                Pelicula = r.Funcion.Pelicula.Titulo,
                Sala = r.Funcion.Sala.Nombre,
                HoraInicio = r.Funcion.HoraInicio,
                HoraFin = r.Funcion.HoraInicio.AddMinutes(r.Funcion.Pelicula.DuracionMinutos),
                CantidadEntradas = r.CantidadEntradas,
                FechaReserva = r.FechaReserva,
                Cliente = new ClienteInfo
                {
                    Nombres = r.Cliente.Nombres,
                    Apellidos = r.Cliente.Apellidos,
                    NumeroDocumento = r.Cliente.NumeroDocumento
                }
            });
        }
    }
}
