﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Service.Queries.DTOs
{
    public class UsuarioDto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set;}
        public string apellido2 { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Guid guid { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public bool admin { get; set; }
        public bool registering { get; set; }
        public string otp { get; set; }
    }
}
