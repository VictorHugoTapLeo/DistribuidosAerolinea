using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineasRafaelPabon.Services
{
    public class ServerSelectionService
    {
        public string SelectServer(string ubicacionCliente)
        {
            // Implementar lógica para seleccionar el servidor más cercano
            // Basado en la ubicación del cliente (simplificado)

            if (ubicacionCliente.Contains("Colombia") || ubicacionCliente.Contains("Venezuela"))
            {
                return "Server1"; // Servidor en América del Sur
            }
            else if (ubicacionCliente.Contains("USA") || ubicacionCliente.Contains("Canada"))
            {
                return "Server2"; // Servidor en Norteamérica
            }
            else
            {
                return "Server3"; // Servidor en Europa/Asia
            }
        }

    }
}
