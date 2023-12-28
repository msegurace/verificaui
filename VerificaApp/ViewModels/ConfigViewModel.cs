namespace VerificaApp.ViewModels
{
    public partial class ConfigViewModel : BaseViewModel
    {

        #region Properties
        [ObservableProperty]
        private string phone;

        [ObservableProperty]
        private string login;

        [ObservableProperty]
        public bool isBiometricsEnabled;
        
        
        #endregion

        #region Constructor
        public ConfigViewModel()
        {
            
            Login = SecureStorage.GetAsync("username").Result;
            Phone = SecureStorage.GetAsync("phone").Result;
            if (SecureStorage.GetAsync("biometricenabled").Result != null) 
            {
                IsBiometricsEnabled = bool.Parse(SecureStorage.GetAsync("biometricenabled").Result);
            }
        }

        #endregion

        #region Methods
        public async void SetBiometrics(bool value)
        {
            await SecureStorage.SetAsync("biometricenabled", value.ToString());
            IsBiometricsEnabled = value;
        }

        [RelayCommand]
        private void GotoSignUp()
        {
            MainThreadHelper.BeginInvokeOnMainThread(async () =>
            {
                TestForRegisteredUser = false;
                await AppShell.Current.GoToAsync($"///{nameof(SignUpPage)}");
            });
        }
        #endregion

        #region Commands

        #endregion
    }
}
