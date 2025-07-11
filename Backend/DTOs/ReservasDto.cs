namespace PruebaTecnicaDomiruth.DTOs
{
    public class ReservaRequest
    {
        public int FuncionId { get; set; }
        public int CantidadEntradas { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Email { get; set; }
    }

    public class ReservaResponse
    {
        public int Id { get; set; }
        public string NumeroTicket { get; set; }
        public string Pelicula { get; set; }
        public string Sala { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public int CantidadEntradas { get; set; }
        public DateTime FechaReserva { get; set; }
        public ClienteInfo Cliente { get; set; }
    }

    public class ClienteInfo
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroDocumento { get; set; }
    }
}
