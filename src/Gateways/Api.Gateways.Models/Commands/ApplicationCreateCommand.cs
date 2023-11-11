namespace Api.Gateways.Models.Commands
{
    public class ApplicationCreateCommand
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public string url { get; set; }
        public string origen { get; set; }
        public string clasificacion_ens { get; set; }
    }
}
