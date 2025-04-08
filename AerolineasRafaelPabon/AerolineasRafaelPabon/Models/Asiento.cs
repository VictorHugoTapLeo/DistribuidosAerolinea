using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using System.ComponentModel;
using System.Diagnostics;

namespace AerolineasRafaelPabon.Models
{
    [Table("Asientos")]
    public class Asiento : INotifyPropertyChanged
    {
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AsientoID { get; set; }

        [Required]
        [StringLength(10)]
        [Index(IsUnique = true)]
        public string CodigoAsiento { get; set; }

        [Required]
        [ForeignKey("Vuelo")]
        public int VueloID { get; set; }

        [Required]
        [StringLength(5)]
        public string NumeroAsiento { get; set; }

        [Required]
        [StringLength(10)]
        public string Tipo { get; set; } // "Ejecutiva" o "Turista"

        [Required]
        [StringLength(10)]
        private string _estado;

        public string Estado
        {
            get => _estado;
            set
            {
                if (_estado != value)
                {
                    _estado = value;
                    OnPropertyChanged(nameof(Estado));
                    Debug.WriteLine($"🎭 Estado cambiado a {value}");
                }
            }
        } // "Libre", "Reservado", "Vendido", "Devolucion"

        [ForeignKey("Pasajero")]
        public int? PasajeroID { get; set; }

        [Required]
        public long Timestamp { get; set; }

        public long? DevolutionExpiry { get; set; }

        // Navegación
        public virtual Vuelo Vuelo { get; set; }
        public virtual Pasajero Pasajero { get; set; }
        public virtual ICollection<Transaccion> Transacciones { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
