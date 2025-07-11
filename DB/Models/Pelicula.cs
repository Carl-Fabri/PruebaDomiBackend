using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DB.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        public int GeneroId { get; set; }

        [ForeignKey("GeneroId")]
        public Genero Genero { get; set; }

        [Required]
        [StringLength(500)]
        public string Sinopsis { get; set; }

        [Required]
        public int DuracionMinutos { get; set; } // Duración en minutos

        [StringLength(200)]
        public string ImagenUrl { get; set; } // Opcional

        public ICollection<Funcion> Funciones { get; set; }
    }

}
