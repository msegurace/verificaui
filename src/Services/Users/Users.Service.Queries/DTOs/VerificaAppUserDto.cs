using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Service.Queries.DTOs
{
    /// <summary>
    /// Representa al usuario conectado y se envía en el cuerpo del mensaje en las llamadas a la API
    /// </summary>
    public class VerificaAppUserDto
    {
        public string uid { get; set; }
        public string? phone { get; set; }
        public string? password { get; set; }
        public bool? registering { get; set; }
        public string? token { get; set; }
        public Guid? guid { get; set; }
        public string? firebase { get; set; }
        public string? otp { get; set; }
    }
}
