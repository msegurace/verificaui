
using CommunityToolkit.Maui.Alerts;

namespace VerificaApp.Views
{
    [QueryProperty(nameof(user), "user")]
    public partial class LoginPage : BasePage
    {
        private LoginViewModel _viewModel;

        public string Name { get { return "LoginPage"; } }

        public VerificaAppUser user
        {
            set
            {
                _viewModel.CurrentUser = value;
            }
        }

        public LoginPage(LoginViewModel loginViewModel) : base(loginViewModel) 
        {
            InitializeComponent();

            _viewModel = loginViewModel;

            Password.Focus();
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.TestAlreadySignedUp(Name);

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