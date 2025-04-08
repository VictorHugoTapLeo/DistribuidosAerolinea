using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AerolineasRafaelPabon.Models
{
    [Table("Naves")]
    public class Nave
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NaveID { get; set; }

        [Required]
        [StringLength(10)]
        [Index(IsUnique = true)]
        public string CodigoNave { get; set; }

        [Required]
        [ForeignKey("TipoNave")]
        public int TipoNaveID { get; set; }

        [Required]
        [StringLength(10)]
        public string Matricula { get; set; }

        public DateTime? UltimoVuelo { get; set; }

        [Required]
        public double HoraVuelo { get; set; } = 0;

        [Required]
        public int CicloVuelo { get; set; } = 0;

        [Required]
        public double DistanciaRecorrida { get; set; } = 0;

        [ForeignKey("UbicacionActual")]
        public int? UbicacionActualID { get; set; }

        // Navegación
        public virtual TipoNave TipoNave { get; set; }
        public virtual Destino UbicacionActual { get; set; }
        public virtual ICollection<Vuelo> Vuelos { get; set; }
    }
}
