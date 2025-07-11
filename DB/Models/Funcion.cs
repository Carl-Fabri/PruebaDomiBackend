using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Funcion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PeliculaId { get; set; }

        [ForeignKey("PeliculaId")]
        public Pelicula Pelicula { get; set; }

        [Required]
        public int SalaId { get; set; }

        [ForeignKey("SalaId")]
        public Sala Sala { get; set; }

        [Required]
        public DateTime HoraInicio { get; set; }

        [Required]
        public int EntradasDisponibles { get; set; }

        public ICollection<Reserva> Reservas { get; set; }
    }

}
