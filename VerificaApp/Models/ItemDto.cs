using System.Text.Json.Serialization;

namespace VerificaApp.Models
{
    public partial class ItemDto : ObservableObject
    {
        //TODO: Cambiar los campos
        public int id { get; set; }
        public DateTime creado { get; set; }
        public DateTime expira { get; set; }
        public string usuario { get; set; }
        public string aplicacion { get; set; }
        public string token { get; set; }
        public bool? aceptado { get; set; }
        public bool? rechazado { get; set; }

        [ObservableProperty]
        private string tiempoExpira;

        [ObservableProperty]
        private bool isExpired;

        [ObservableProperty]
        private Color expiryColor;

        private DateTime Fecha_Expira;

        IDispatcherTimer timer;

        public ItemDto()
        {
            ExpiryColor = Color.FromArgb("ffffff");
            TiempoExpira = "";
            SetTiempoExpira();
        }

        //TODO: Ver para qué sirve esta clase
        [JsonSerializable(typeof(ObservableCollection<ItemDto>))]
        internal sealed partial class TokensAppContext : JsonSerializerContext
        {

        }

        private void SetTiempoExpira()
        {
            timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => TimerTick();
            timer.Start();
        }

        private void TimerTick()
        {
            var distance = (expira - DateTime.Now).TotalMilliseconds;
            var days = Math.Floor(distance / (1000 * 60 * 60 * 24));
            var hours = Math.Floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.Floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.Floor((distance % (1000 * 60)) / 1000);

            if (distance <= 0)
            {
                TiempoExpira = "Tiempo finalizado";
                IsExpired = true;
                timer.Stop();
            }
            else
            {
                TiempoExpira = string.Concat(minutes.ToString("00"), ":", seconds.ToString("00"));
                IsExpired = false;
                if (minutes == 1)
                {
                    ExpiryColor = Color.FromArgb("FF5733");
                }
                else if (minutes == 0)
                {
                    ExpiryColor = Color.FromArgb("f5001a");
                }
                else
                {
                    ExpiryColor = Color.FromArgb("ffffff");
                }
            }


        }

        public bool FoundAuth(ItemDto obj)
        {
            return (aplicacion.Equals(obj.aplicacion) &&
                creado == obj.creado);               
        }
    }
}
