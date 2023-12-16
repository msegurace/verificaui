
using CommunityToolkit.Maui.Alerts;

namespace VerificaApp.Views
{
    public partial class LoginPage : BasePage
    {

        public string Name { get { return "LoginPage"; } }

        private LoginViewModel _viewModel;
        public LoginPage(LoginViewModel loginViewModel) : base(loginViewModel) 
        {
            InitializeComponent();

            _viewModel = loginViewModel;

            _viewModel.TestAlreadySignedUp(Name);

            Password.Focus();
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.BiometricsAvailable();
            if (_viewModel.IsBiometricsEnabled)
            {
                _viewModel.FingerPrintCommand.Execute(null);
            }
        }

        private void TapGestureRecognizerUsuario_Tapped(object sender, TappedEventArgs e)
        {
            var snackBar = Snackbar.Make(
                CommonConstants.INFO_USER_PASSWORD,
                action: null,
                actionButtonText: "Cerrar",
                duration: TimeSpan.FromSeconds(15),
                new CommunityToolkit.Maui.Core.SnackbarOptions
                {
                    BackgroundColor = Color.FromRgb(7, 103, 172),
                    TextColor = Colors.White
                },
                (sender as VisualElement));
            snackBar.Show();
        }
    }
}