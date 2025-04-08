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
    [Table("Pasajeros")]
    public class Pasajero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PasajeroID { get; set; }

        [Required]
        [Range(10000, 99999)]
        [Index(IsUnique = true)]
        public int Pasaporte { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCompleto { get; set; }

        // Navegación
        public virtual ICollection<Asiento> Asientos { get; set; }
        public virtual ICollection<Transaccion> Transacciones { get; set; }
    }
}
