using System.ComponentModel;

namespace VerificaApp.Models
{
    public partial class AuthRequestDTO : BaseViewModel
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string Token { get; set; }
        public bool Aceptado { get; set; }
        public string Aplicacion { get; set; }
        public string Ip { get; set; }
        public DateTime Fecha_creado { get; set; }
        public DateTime Fecha_expira { get; set; }

        [ObservableProperty]
        private string tiempoExpira;

        [ObservableProperty]
        private bool isExpired;

        [ObservableProperty]
        private Color expiryColor;


        public AuthRequestDTO()
        {
            ExpiryColor = Colors.Red;
            TiempoExpira = "";
            SetTiempoExpira();
        }



        private void SetTiempoExpira()
        {

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                var distance = ((DateTime)Fecha_expira - DateTime.Now).TotalMilliseconds;
                var days = Math.Floor(distance / (1000 * 60 * 60 * 24));
                var hours = Math.Floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                var minutes = Math.Floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                var seconds = Math.Floor((distance % (1000 * 60)) / 1000);

                if (distance <= 0)
                {
                    TiempoExpira = "Tiempo finalizado";
                    IsExpired = true;
                    return false;
                }
                else
                {
                    TiempoExpira = string.Concat(minutes.ToString("00"), ":", seconds.ToString("00"));
                    IsExpired = false;
                    if (minutes == 1)
                    {
                        ExpiryColor = Colors.Orange;
                    } 
                    else if (minutes == 0)
                    {
                        ExpiryColor = Colors.Red;
                    }
                    else
                    {
                        ExpiryColor = Colors.White;
                    }
                    return true; // return true to repeat counting, false to stop timer
                }

                    // TiempoExpira = d.Add(TimeSpan.FromSeconds(-1)).ToString("mm:ss");
                    // return true;
                    /* DispatcherTimer _timer = new DispatcherTimer();
                     TimeSpan _time = new TimeSpan();
                     _time = TimeSpan.FromSeconds(mincount);
                     _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                     {
                         TimeNow = _time.ToString("c");
                         if (_time == TimeSpan.Zero) _timer.Stop();
                         _time = _time.Add(TimeSpan.FromSeconds(-1));
                     }, Application.Current.Dispatcher);
                     _timer.Start();
                    */
            });
        }

        public bool FoundAuth(AuthRequestDTO obj)
        {
            return (this.Aplicacion.Equals(obj.Aplicacion) &&
                this.Ip.Equals(obj.Ip) &&
                this.Fecha_creado == obj.Fecha_creado);               
        }     

    }
}
