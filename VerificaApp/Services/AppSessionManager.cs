namespace VerificaApp.Services
{
    public sealed class AppSessionManager
    {
        private static readonly Lazy<AppSessionManager> lazy = new Lazy<AppSessionManager>();
        public static AppSessionManager Instance { get { return lazy.Value; } }
        private Stopwatch StopWatch = new Stopwatch();

        private readonly int _sessionThreasholdMinutes = CommonConstants.SessionExpiration;

        private IDispatcherTimer timer;

        public AppSessionManager()
        {
            SessionDuration = TimeSpan.FromMinutes(_sessionThreasholdMinutes);
        }

        private TimeSpan SessionDuration;

        public void EndSession()
        {
            if (StopWatch.IsRunning)
            {
                StopWatch.Stop();
            }
        }

        public void ExtendSession()
        {
            if (StopWatch.IsRunning)
            {
                StopWatch.Restart();
            }
        }

        public void StartSession()
        {
            if (!StopWatch.IsRunning)
            {
                StopWatch.Restart();
            }

            timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => TimerTick();
            timer.Start();
        }

        private void TimerTick()
        {
            Console.WriteLine("Session Started at " + DateTime.Now.ToLongTimeString());

            if (StopWatch.IsRunning && StopWatch.Elapsed.Minutes >= SessionDuration.Minutes) //User was inactive for N minutes
            {
                RedirectAndInformInactivity();
                EndSession();

            }
            Console.WriteLine("Current Time Elapsed -" + StopWatch.Elapsed.ToString());

        }

        //TODO
        private void RedirectAndInformInactivity()
        {
            MainThreadHelper.BeginInvokeOnMainThread(async () =>
            {
                EndSession();
                await Shell.Current.GoToAsync($"//{nameof(ExpiredSession)}");
            });
        }
    }
}