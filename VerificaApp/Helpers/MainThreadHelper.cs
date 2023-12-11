using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerificaApp.Helpers
{
    public static class MainThreadHelper
    {
        public static void BeginInvokeOnMainThread(Action action)
        {
            if (DeviceInfo.Platform == DevicePlatform.Unknown)
            {
                Task.Run(action);
                return;
            }

            MainThread.BeginInvokeOnMainThread(action);
        }


        public static bool IsMainThread
        {
            get
            {
                if (DeviceInfo.Platform == DevicePlatform.Unknown)
                {
                    return true;
                }

                return MainThread.IsMainThread;
            }
        }
    }
}
