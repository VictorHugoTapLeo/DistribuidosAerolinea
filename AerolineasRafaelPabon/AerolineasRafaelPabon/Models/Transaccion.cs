using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AerolineasRafaelPabon.Models
{
    [Table("Transacciones")]
    public class Transaccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransaccionID { get; set; }

        [Required]
        [StringLength(10)]
        [Index(IsUnique = true)]
        public string CodigoTransaccion { get; set; }

        [Required]
        [ForeignKey("Vuelo")]
        public int VueloID { get; set; }

        [Required]
        [ForeignKey("Asiento")]
        public int AsientoID { get; set; }

        [Required]
        [ForeignKey("Pasajero")]
        public int? PasajeroID { get; set; }

        [Required]
        [StringLength(10)]
        public string Tipo { get; set; } // "Reserva", "Venta", "Devolucion"

        [Required]
        [StringLength(10)]
        public string Estado { get; set; } // "Activo", "Anulado"

        [Required]
        public long Timestamp { get; set; }

        public string VectorClock { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Costo { get; set; }

        // Navegación
        public virtual Vuelo Vuelo { get; set; }
        public virtual Asiento Asiento { get; set; }
        public virtual Pasajero Pasajero { get; set; }
    }
}
