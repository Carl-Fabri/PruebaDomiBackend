using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionId { get; set; }

        [ForeignKey("FuncionId")]
        public Funcion Funcion { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Required]
        public DateTime FechaReserva { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroTicket { get; set; }

        [Required]
        public int CantidadEntradas { get; set; }

    }
}
