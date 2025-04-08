using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AerolineasRafaelPabon.Services
{
    public class VectorClockService
    {
        private Dictionary<string, int> _vectorClock = new Dictionary<string, int>();

        public VectorClockService(string serverId)
        {
            // Inicializar el reloj vectorial para este servidor
            _vectorClock.Add(serverId, 0);
        }

        public void Increment(string serverId)
        {
            if (_vectorClock.ContainsKey(serverId))
            {
                _vectorClock[serverId]++;
            }
            else
            {
                _vectorClock.Add(serverId, 1);
            }
        }

        public string GetCurrentClock()
        {
            return JsonConvert.SerializeObject(_vectorClock);
        }

        public void UpdateClock(Dictionary<string, int> receivedClock)
        {
            foreach (var entry in receivedClock)
            {
                if (_vectorClock.ContainsKey(entry.Key))
                {
                    _vectorClock[entry.Key] = Math.Max(_vectorClock[entry.Key], entry.Value);
                }
                else
                {
                    _vectorClock.Add(entry.Key, entry.Value);
                }
            }
        }
    }
}
