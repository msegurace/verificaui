using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Service.Queries.DTOs
{
    /// <summary>
    /// Clase para devolver la respuesta de un intento de validación
    /// </summary>
    public class VerificaGenericResponse
    {
        public string code { get; set; }
        public object content { get; set; }
    }
}
