using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaDomiruth.DTOs
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroDocumento { get; set; }
        public string Email { get; set; }
    }

    public class ClienteCreateDto
    {
        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public string TipoDocumento { get; set; }

        [Required]
        public string NumeroDocumento { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
