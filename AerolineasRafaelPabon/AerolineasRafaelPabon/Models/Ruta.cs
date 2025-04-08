using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AerolineasRafaelPabon.Models
{
    [Table("Rutas")]
    public class Ruta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RutaID { get; set; }

        [Required]
        [StringLength(10)]
        [Index(IsUnique = true)]
        public string CodigoRuta { get; set; }

        [Required]
        [ForeignKey("Origen")]
        public int OrigenID { get; set; }

        [Required]
        [ForeignKey("Destino")]
        public int DestinoID { get; set; }

        [Required]
        public double Distancia { get; set; }

        [Required]
        public TimeSpan Tiempo { get; set; }

        // Navegación
        public virtual Destino Origen { get; set; }
        public virtual Destino Destino { get; set; }
        public virtual ICollection<Vuelo> Vuelos { get; set; }
    }
}
