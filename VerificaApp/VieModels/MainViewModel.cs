using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerificaApp.VieModels
{
    /// <summary>
    /// 
    /// </summary>
    public class MainViewModel
    {
        public bool TestForRegisteredUser { get; set; }

        public MainViewModel()
        {
           TestForRegisteredUser = true;
        }
    }
}
