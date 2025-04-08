using AerolineasRafaelPabon.Services;
using System.Configuration;
using System.Data;
using System.Windows;

namespace AerolineasRafaelPabon
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DevolucionService _devolucionService;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _devolucionService = new DevolucionService();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _devolucionService?.Dispose();
            base.OnExit(e);
        }
    }

}
