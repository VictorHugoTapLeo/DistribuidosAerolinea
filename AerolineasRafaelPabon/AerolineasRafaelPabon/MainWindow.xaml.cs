using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using AerolineasRafaelPabon.Models;
using AerolineasRafaelPabon.Services;
using System.Data.Entity; // Para DbFunctions
using System.Configuration;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel; // Para ConfigurationManager


namespace AerolineasRafaelPabon
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private VectorClockService _vectorClock;
        private string _serverId = "Server1"; // Cambiar según la ubicación
        private DevolucionService _devolucionService;
        private int? _vueloSeleccionadoId; // Variable para guardar el ID del vuelo seleccionado
        private Vuelo _vueloSeleccionado; // Variable para guardar el objeto completo del vuelo

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _vectorClock = new VectorClockService(_serverId);
            _devolucionService = new DevolucionService();
            _devolucionService.AsientoLiberado += OnAsientoLiberado;
            CargarDatosIniciales();
            // Hora de Bolivia por defecto (UTC-4)
            ActualizarHora("SA Western Standard Time");
        }

        private void OnAsientoLiberado(int vueloId)
        {
            Dispatcher.Invoke(() =>
            {
                // Verificar contra el vuelo guardado en lugar de la selección actual
                if (_vueloSeleccionadoId.HasValue && _vueloSeleccionadoId.Value == vueloId)
                {
                    Debug.WriteLine($"Actualizando asientos para vuelo {vueloId} por liberación automática");
                    CargarAsientos(vueloId);
                }
            });
        }

        private void CargarDatosIniciales()
        {
            // Cargar destinos
            using (var context = new AerolineasContext())
            {
                var destinos = context.Destinos.ToList();
                cbOrigen.ItemsSource = destinos;
                cbDestino.ItemsSource = destinos;
            }

            dpFecha.SelectedDate = DateTime.Today;
        }
        private DispatcherTimer _timer;
        private void ActualizarHora(string zonaHoraria = null)
        {
            // Detener el timer si ya existe
            if (_timer != null)
            {
                _timer.Stop();
            }

            // Crear un nuevo timer
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (sender, args) =>
            {
                var ahora = DateTime.UtcNow;
                if (!string.IsNullOrEmpty(zonaHoraria))
                {
                    try
                    {
                        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(zonaHoraria);
                        ahora = TimeZoneInfo.ConvertTimeFromUtc(ahora, timeZone);
                    }
                    catch
                    {
                        // Si hay error, usar hora local
                        ahora = DateTime.Now;
                    }
                }
                else
                {
                    ahora = DateTime.Now;
                }
                txtHoraActual.Text = ahora.ToString("dd MMM HH:mm:ss");
            };
            _timer.Start();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (cbOrigen.SelectedItem == null || cbDestino.SelectedItem == null || dpFecha.SelectedDate == null)
            {
                MessageBox.Show("Por favor seleccione origen, destino y fecha");
                return;
            }

            var origenId = ((Destino)cbOrigen.SelectedItem).DestinoID;
            var destinoId = ((Destino)cbDestino.SelectedItem).DestinoID;
            var fecha = dpFecha.SelectedDate.Value;

            using (var context = new AerolineasContext())
            {
                var vuelos = context.Vuelos
    .Include(v => v.Ruta)
    .Include(v => v.Ruta.Origen)
    .Include(v => v.Ruta.Destino)
    .Include(v => v.Nave)
    .Where(v => v.Ruta.OrigenID == origenId &&
               v.Ruta.DestinoID == destinoId &&
               System.Data.Entity.DbFunctions.TruncateTime(v.FechaSalida) == fecha.Date)
    .ToList();

                dgVuelos.ItemsSource = vuelos;
            }
        }

        private void dgVuelos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgVuelos.SelectedItem is Vuelo vueloSeleccionado)
            {
                // Guardar tanto el ID como el objeto completo del vuelo
                _vueloSeleccionadoId = vueloSeleccionado.VueloID;
                _vueloSeleccionado = vueloSeleccionado;

                CargarAsientos(vueloSeleccionado.VueloID);
                tabAsientos.IsSelected = true;
            }
        }

        private void CargarAsientos(int vueloId)
        {
            try
            {
                // Actualizar el ID guardado por si acaso
                _vueloSeleccionadoId = vueloId;

                using (var context = new AerolineasContext())
                {
                    var asientos = context.Asientos
                        .Include(a => a.Pasajero)
                        .Where(a => a.VueloID == vueloId)
                        .OrderBy(a => a.NumeroAsiento)
                        .ToList();

                    Asientos = new ObservableCollection<Asiento>(asientos);
                    icAsientos.ItemsSource = Asientos;

                    Debug.WriteLine($"Asientos actualizados: {asientos.Count} para vuelo {vueloId}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar asientos: {ex.Message}");
            }
        }

        private void AsientoButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var asiento = (Asiento)button.DataContext;

            var ventanaAsiento = new AsientoWindow(asiento);
            if (ventanaAsiento.ShowDialog() == true)
            {
                // Usar el vuelo guardado en lugar del asiento.VueloID por si hubo cambios
                if (_vueloSeleccionadoId.HasValue)
                {
                    CargarAsientos(_vueloSeleccionadoId.Value);
                }
            }
        }
        private void cbUbicacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbUbicacion.SelectedItem != null)
            {
                var ubicacion = ((ComboBoxItem)cbUbicacion.SelectedItem).Content.ToString();
                var zonaHoraria = ObtenerZonaHoraria(ubicacion);
                ActualizarHora(zonaHoraria);
            }
        }

        private string ObtenerZonaHoraria(string ubicacion)
        {
            if (ubicacion.Contains("Bogotá") || ubicacion.Contains("Colombia"))
                return "SA Pacific Standard Time"; // UTC-5 (sin cambios)

            if (ubicacion.Contains("Caracas") || ubicacion.Contains("Venezuela"))
                return "Venezuela Standard Time"; // UTC-4 (igual que Bolivia)

            if (ubicacion.Contains("Miami") || ubicacion.Contains("USA"))
                return "Eastern Standard Time"; // UTC-5 (normal) o UTC-4 (horario de verano)

            // Si no es ninguna de las anteriores (ej: Bolivia por defecto)
            return "SA Western Standard Time"; // UTC-4 (Bolivia)
        }

        public void ActualizarAsientos()
        {
            // Usar el vuelo guardado en lugar de depender de la selección actual
            if (_vueloSeleccionadoId.HasValue)
            {
                Debug.WriteLine($"🔄 Refrescando lista de asientos para vuelo {_vueloSeleccionadoId}...");

                // Fuerza una recarga completa
                icAsientos.ItemsSource = null;

                using (var context = new AerolineasContext())
                {
                    icAsientos.ItemsSource = context.Asientos
                        .Where(a => a.VueloID == _vueloSeleccionadoId.Value)
                        .OrderBy(a => a.NumeroAsiento)
                        .AsEnumerable()
                        .Select(a => new Asiento
                        {
                            AsientoID = a.AsientoID,
                            Estado = a.Estado,
                            NumeroAsiento = a.NumeroAsiento,
                            // ... otras propiedades ...
                        })
                        .ToList();
                }

                Debug.WriteLine("🎨 Lista de asientos actualizada");
            }
        }

        // Modifica el método Dispose
        protected override void OnClosed(EventArgs e)
        {
            _devolucionService?.Dispose();
            base.OnClosed(e);
        }

        private ObservableCollection<Asiento> _asientos;
        public ObservableCollection<Asiento> Asientos
        {
            get => _asientos;
            set
            {
                _asientos = value;
                OnPropertyChanged(nameof(Asientos));
            }
        }

        // Implementa INotifyPropertyChanged en MainWindow
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

}