using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerificaApp.Helpers
{
    public interface ISMSHandler

    {
        Task<bool> RequestPermissions();
        Task<List<string>> getAllSms();
    }
}
