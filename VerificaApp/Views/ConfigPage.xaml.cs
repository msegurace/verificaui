namespace VerificaApp.Views
{
    public partial class ConfigPage : BasePage
    {
        private ConfigViewModel _configViewModel; 

        public ConfigPage(ConfigViewModel configViewModel)
        {
            InitializeComponent();

            _configViewModel = configViewModel;

        }

        private void BiometricAccess_Toggled(object sender, ToggledEventArgs e)
        {
            _configViewModel.SetBiometrics(e.Value);
        }
    }
}