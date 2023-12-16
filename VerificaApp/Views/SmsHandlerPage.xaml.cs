
namespace VerificaApp.Views
{
    [QueryProperty(nameof(user), "user")]
    public partial class SmsHandlerPage : BasePage
    {
        private SmsHandlerViewModel _viewModel;
        private ISMSHandler _smsHandler;
        DateTimeOffset currentTime = new DateTimeOffset(DateTime.Now);
        IDispatcherTimer timer = null;
        string sms = String.Empty;
        public VerificaAppUser user { 
            set
            {
                _viewModel.CurrentUser = value;
            }
        }

        public SmsHandlerPage(SmsHandlerViewModel viewModel, ISMSHandler smsHandler)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _smsHandler = smsHandler;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //  DisplayAlert("SMS", MainViewModel.Instance.ConnectedUser.otp, "OK");
            _smsHandler.RequestPermissions();

            timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += (s, e) => GetSMS();
            timer.Start();
        }

        private async Task GetSMS()
        {
            MainThreadHelper.BeginInvokeOnMainThread(async () =>
            {
                var smsList = await _smsHandler.getAllSms();
                
                foreach (var item in smsList)
                {
                    var splitItem = item.Split("#");
                    if (splitItem[1].StartsWith(CommonConstants.SMS_START))
                    {
                        var date = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(splitItem[0])).LocalDateTime;
                        if (date > currentTime)
                        {
                            _viewModel.SmsActivationCode = splitItem[1].Split(":")[splitItem.Length].Trim();
                            pinBox.Text = _viewModel.SmsActivationCode;
                            timer.Stop();
                            break;
                        }
                    }
                }
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _viewModel.ValidateOtpCommand.Execute(null);
        }

        private void pinBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SmsActivationCode = pinBox.Text;
        }

        //private void PinBox_Completed(object sender, TemplateUI.Controls.PinCompletedEventArgs e)
        //{
        //    if (String.IsNullOrEmpty(vm.ActivationCode))
        //    {
        //        _viewModel.ActivationCode = e.Password;
        //    }
        //    // DisplayAlert("PinBox", $"Pin completed: {e.Password}", "Ok");
        //    _viewModel.ValidateOTP.Execute(null);
        //}
    }
}