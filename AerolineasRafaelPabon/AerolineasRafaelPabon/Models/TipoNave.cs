using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AerolineasRafaelPabon.Models
{
    [Table("TiposNaves")]
    public class TipoNave
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoNaveID { get; set; }

        [Required]
        [StringLength(10)]
        [Index(IsUnique = true)]
        public string CodigoTipoNave { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(20)]
        public string Clase { get; set; }

        [Required]
        public int CapacidadEjecutiva { get; set; }

        [Required]
        public int CapacidadTurista { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal CostoTurista { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal CostoEjecutivo { get; set; }

        // Navegación
        public virtual ICollection<Nave> Naves { get; set; }
    }
}
