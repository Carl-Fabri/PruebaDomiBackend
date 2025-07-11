using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaDomiruth.DTOs
{
    public class PeliculaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public string Sinopsis { get; set; }
        public int DuracionMinutos { get; set; }
        public string ImagenUrl { get; set; }
    }

    public class PeliculaDetailDto : PeliculaDto
    {
        public List<FuncionDto> Funciones { get; set; }
    }

    public class CarteleraFilterDto
    {
        public DateTime? Fecha { get; set; }
        public int? GeneroId { get; set; }
        public string? Titulo { get; set; }
    }

    public class PeliculaCreateDto
    {
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El género es requerido")]
        public int GeneroId { get; set; }

        [Required(ErrorMessage = "La sinopsis es requerida")]
        [StringLength(1000, ErrorMessage = "La sinopsis no puede exceder 1000 caracteres")]
        public string Sinopsis { get; set; }

        [Required(ErrorMessage = "La duración es requerida")]
        [Range(1, 500, ErrorMessage = "La duración debe estar entre 1 y 500 minutos")]
        public int DuracionMinutos { get; set; }

        [StringLength(500, ErrorMessage = "La URL de imagen no puede exceder 500 caracteres")]
        public string? ImagenUrl { get; set; }
    }
}
