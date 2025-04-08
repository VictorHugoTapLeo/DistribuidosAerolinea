using AerolineasRafaelPabon.Models;
using AerolineasRafaelPabon.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;

namespace AerolineasRafaelPabon
{
    /// <summary>
    /// Lógica de interacción para AsientoWindow.xaml
    /// </summary>
    public partial class AsientoWindow : Window
    {
        private readonly AerolineasContext _context = new AerolineasContext();
        public Asiento Asiento { get; set; }

        public AsientoWindow(Asiento asiento)
        {
            InitializeComponent();
            Asiento = _context.Asientos.Find(asiento.AsientoID); // Recargar desde DB
            DataContext = Asiento;
            ConfigurarBotonesSegunEstado();
        }

        private void ConfigurarBotonesSegunEstado()
        {
            btnComprar.IsEnabled = false;
            btnReservar.IsEnabled = false;
            btnDevolver.IsEnabled = false;

            switch (Asiento.Estado)
            {
                case "Libre":
                    btnComprar.IsEnabled = true;
                    btnReservar.IsEnabled = true;
                    break;
                case "Reservado":
                    btnComprar.IsEnabled = true;
                    btnDevolver.IsEnabled = true;
                    break;
                case "Devolucion":
                    btnComprar.IsEnabled = true;
                    break;
            }
        }

        private void txtPasaporte_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(txtPasaporte.Text, out int pasaporte) && pasaporte >= 10000 && pasaporte <= 99999)
            {
                var pasajero = _context.Pasajeros.FirstOrDefault(p => p.Pasaporte == pasaporte);
                txtNombrePasajero.Text = pasajero != null ? pasajero.NombreCompleto : "Nuevo pasajero";
            }
            else
            {
                txtNombrePasajero.Text = "";
            }
        }

        private void RealizarOperacion(string tipoOperacion)
        {
            if (!int.TryParse(txtPasaporte.Text, out int pasaporte) || pasaporte < 10000 || pasaporte > 99999)
            {
                MessageBox.Show("Pasaporte inválido. Debe ser un número de 5 dígitos.");
                return;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Buscar o crear pasajero
                    var pasajero = _context.Pasajeros.FirstOrDefault(p => p.Pasaporte == pasaporte);
                    if (pasajero == null)
                    {
                        pasajero = new Pasajero
                        {
                            Pasaporte = pasaporte,
                            NombreCompleto = txtNombrePasajero.Text
                        };
                        _context.Pasajeros.Add(pasajero);
                        _context.SaveChanges();
                    }

                    // Actualizar estado del asiento
                    var asientoActual = _context.Asientos.Find(Asiento.AsientoID);
                    
                    switch (tipoOperacion)
                    {
                        case "Venta":
                            asientoActual.Estado = "Vendido";
                            asientoActual.PasajeroID = pasajero.PasajeroID;
                            break;
                        case "Reserva":
                            asientoActual.Estado = "Reservado";
                            asientoActual.PasajeroID = pasajero.PasajeroID;
                            break;
                        case "Devolucion":
                            asientoActual.Estado = "Devolucion";
                            asientoActual.DevolutionExpiry = DateTimeOffset.UtcNow.AddMinutes(1).ToUnixTimeSeconds();
                            break;
                    }

                    asientoActual.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                    // Registrar transacción
                    _context.Transacciones.Add(new Transaccion
                    {
                        CodigoTransaccion = Guid.NewGuid().ToString().Substring(0, 10),
                        VueloID = asientoActual.VueloID,
                        AsientoID = asientoActual.AsientoID,
                        PasajeroID = pasajero.PasajeroID,
                        Tipo = tipoOperacion,
                        Estado = "Activo",
                        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                        VectorClock = "{}",
                        Costo = CalcularCosto(asientoActual, tipoOperacion)
                    });

                    _context.SaveChanges();
                    transaction.Commit();
                    DialogResult = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Error al realizar la operación: {ex.Message}");
                }
            }
        }

        private decimal CalcularCosto(Asiento asiento, string tipoOperacion)
        {
            if (tipoOperacion == "Devolucion") return 0;

            var vuelo = _context.Vuelos
                .Include(v => v.Nave)
                .Include(v => v.Nave.TipoNave)
                .First(v => v.VueloID == asiento.VueloID);

            return asiento.Tipo == "Ejecutiva" 
                ? vuelo.Nave.TipoNave.CostoEjecutivo 
                : vuelo.Nave.TipoNave.CostoTurista;
        }

        private void btnComprar_Click(object sender, RoutedEventArgs e) => RealizarOperacion("Venta");
        private void btnReservar_Click(object sender, RoutedEventArgs e) => RealizarOperacion("Reserva");
        private void btnDevolver_Click(object sender, RoutedEventArgs e) => RealizarOperacion("Devolucion");
        private void btnCancelar_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}