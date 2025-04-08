using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace AerolineasRafaelPabon.Models
{
    [Table("Vuelos")]
    public class Vuelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VueloID { get; set; }

        [Required]
        [StringLength(10)]
        [Index(IsUnique = true)]
        public string CodigoVuelo { get; set; }

        [Required]
        [ForeignKey("Nave")]
        public int NaveID { get; set; }

        [Required]
        [ForeignKey("Ruta")]
        public int RutaID { get; set; }

        [Required]
        public DateTime FechaSalida { get; set; }

        [Required]
        public DateTime FechaLlegada { get; set; }

        [Required]
        public TimeSpan HoraSalida { get; set; }

        [Required]
        public TimeSpan HoraLlegada { get; set; }

        [StringLength(10)]
        public string PuertaSalida { get; set; }

        [StringLength(10)]
        public string PuertaLlegada { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; } = "Abierto";

        // Navegación
        public virtual Nave Nave { get; set; }
        public virtual Ruta Ruta { get; set; }
        public virtual ICollection<Asiento> Asientos { get; set; }
        public virtual ICollection<Transaccion> Transacciones { get; set; }
    }
}
