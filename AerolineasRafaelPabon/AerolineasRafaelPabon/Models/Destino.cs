using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AerolineasRafaelPabon.Models
{
    [Table("Destinos")]
    public class Destino
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DestinoID { get; set; }

        [Required]
        [StringLength(10)]
        [Index(IsUnique = true)]
        public string CodigoDestino { get; set; }

        [Required]
        [StringLength(50)]
        public string Pais { get; set; }

        [Required]
        [StringLength(50)]
        public string Ciudad { get; set; }

        [Required]
        [StringLength(100)]
        public string Aeropuerto { get; set; }

        [StringLength(10)]
        public string ShortDescription { get; set; }

        // Navegación
        public virtual ICollection<Ruta> RutasOrigen { get; set; }
        public virtual ICollection<Ruta> RutasDestino { get; set; }
        public virtual ICollection<Nave> Naves { get; set; }
    }
}
