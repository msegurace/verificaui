using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Queries.DTOs
{
    public class AplicacionDto
    {
        public int id { get; set; }
        public string descripcion{ get; set; }
        public string url { get; set;}
        public string origen { get; set; }
        public string clasificacion_ens { get; set; }

    }
}
