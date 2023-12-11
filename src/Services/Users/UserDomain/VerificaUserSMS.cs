using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDomain
{
    /// <summary>
    /// Representa un registro de la tabla user_sms de la base de datos.
    /// </summary>
    [Table("user_sms")]
    public class VerificaUserSMS
    {
        [Key]
        public long id { get; set; }

        public string uid { get; set; }

        public string otp { get; set; }

        public DateTime expiration_date { get; set; }
    }
}
