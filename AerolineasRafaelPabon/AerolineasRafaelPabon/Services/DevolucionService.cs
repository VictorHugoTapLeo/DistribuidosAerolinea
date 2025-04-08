using AerolineasRafaelPabon.Models;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows; // Agrega este using para Application

namespace AerolineasRafaelPabon.Services
{

    public class DevolucionService : IDisposable
    {
        private readonly System.Timers.Timer _timer;
        private readonly object _lock = new object();
        public event Action<int> AsientoLiberado;

        public DevolucionService()
        {
            _timer = new System.Timers.Timer(30000); // 30 segundos
            _timer.Elapsed += ProcesarDevolucionesExpiradas;
            _timer.Start();
        }

        // En DevolucionService.cs
        private void ProcesarDevolucionesExpiradas(object sender, ElapsedEventArgs e)
        {
            lock (_lock)
            {
                try
                {
                    var ahora = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                    using (var context = new AerolineasContext())
                    {
                        var asientos = context.Asientos
                    .Include(a => a.Vuelo)
                    .Include(a => a.Pasajero)
                    .Where(a => a.Estado == "Devolucion" && a.DevolutionExpiry <= ahora)
                    .ToList();


                        foreach (var asiento in asientos)
                        {
                            asiento.Estado = "Libre";
                            asiento.DevolutionExpiry = null;
                            asiento.Timestamp = ahora;
                        }

                        if (context.SaveChanges() > 0)
                        {
                            // Notificar a la UI para actualizar
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                var mainWindow = Application.Current.MainWindow as MainWindow;
                                mainWindow?.ActualizarAsientos();
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error al procesar devoluciones: {ex.Message}");
                }
            }
        }

        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Dispose();
            Debug.WriteLine("Servicio de devoluciones detenido");
        }
    }
}