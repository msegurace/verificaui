using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDomain 
{ 
    public class AuthInfo
    {
        public string username { get; set; }
        public string password { get; set; }

        public AuthInfo()
        {

        }

        public AuthInfo(string username, string password, string auth_token)
        {
            this.username = username;
            this.password = password;
        }

        
    }
}
