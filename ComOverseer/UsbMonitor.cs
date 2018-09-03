using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace ComOverseer
{
    public class UsbMonitor : IDisposable
    {
        private ManagementEventWatcher insertWatcher;
        public event EventArrivedEventHandler OnInsert_handler = delegate { };

        public UsbMonitor()
        {
            insertWatcher = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2 or EventType = 3"));
            insertWatcher.EventArrived += new EventArrivedEventHandler(this.OnInsert);
            insertWatcher.Start();
        }

        private void OnInsert(Object s, EventArrivedEventArgs e)
        {
            this.OnInsert_handler(s, e);
        }

        #region IDisposable Support
        private bool disposedValue = false; // Aby wykryć nadmiarowe wywołania

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: wyczyść stan zarządzany (obiekty zarządzane).
                }

                // TODO: Zwolnić niezarządzane zasoby (niezarządzane obiekty) i przesłonić poniższy finalizator.
                // TODO: ustaw wartość null dla dużych pól.

                disposedValue = true;
            }
        }

        // TODO: Przesłonić finalizator tylko w sytuacji, gdy powyższa metoda Dispose(bool disposing) zawiera kod służący do zwalniania niezarządzanych zasobów.
        // ~UsbMonitor() {
        //   // Nie zmieniaj tego kodu. Umieść kod czyszczący w powyższej metodzie Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Ten kod został dodany w celu prawidłowego zaimplementowania wzorca rozporządzającego.
        public void Dispose()
        {
            // Nie zmieniaj tego kodu. Umieść kod czyszczący w powyższej metodzie Dispose(bool disposing).
            Dispose(true);
            // TODO: Usunąć komentarz z poniższego wiersza, jeśli finalizator został przesłonięty powyżej.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
