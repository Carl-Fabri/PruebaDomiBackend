using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellidos { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(20)]
        public string Genero { get; set; } // Masculino, Femenino, Otro

        [Required]
        [StringLength(20)]
        public string TipoDocumento { get; set; } // DNI, Pasaporte, etc.

        [Required]
        [StringLength(20)]
        public string NumeroDocumento { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public ICollection<Reserva> Reservas { get; set; }
    }
}
