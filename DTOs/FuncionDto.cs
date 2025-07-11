using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaDomiruth.DTOs
{
    public class FuncionDto
    {
        public int Id { get; set; }
        public string Pelicula { get; set; }
        public string Sala { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public int EntradasDisponibles { get; set; }
        public int DuracionPelicula { get; set; }
    }

    public class FuncionCreateDto
    {
        [Required]
        public int PeliculaId { get; set; }

        [Required]
        public int SalaId { get; set; }

        [Required]
        public DateTime HoraInicio { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int EntradasDisponibles { get; set; }
    }
}
