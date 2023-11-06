using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Models
{
    public class AuthInfo
    {
        public string username { get; set; }
        public string password { get; set; }
        public string auth_token { get; set; }

        public AuthInfo()
        {

        }

        public AuthInfo(string username, string password, string auth_token)
        {
            this.username = username;
            this.password = password;
            this.auth_token = auth_token;
        }

        
    }
}
