using MediatR;

namespace User.Service.EventHandlers.Commands
{
    public class ApplicationCreateCommand: INotification
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public string url { get; set; }
        public string origen { get; set; }
        public string clasificacion_ens { get; set; }
    }
}
