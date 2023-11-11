using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenDomain
{
    [Table("token")]
    public class Token2FA
    {
        [Key]
        public int id { get; set; }
        public DateTime creado { get; set; }
        public int idusuario { get; set; }
        public int idaplicacion { get; set; }
        public string token { get; set; }
        public bool aceptado { get; set; }
        public bool rechazado { get; set; }
    }
}
