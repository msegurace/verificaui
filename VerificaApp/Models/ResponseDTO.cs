using System;
using System.Collections.Generic;
using System.Text;

namespace VerificaApp.Models
{
   
        public class ResponseDTO
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }
    
}
