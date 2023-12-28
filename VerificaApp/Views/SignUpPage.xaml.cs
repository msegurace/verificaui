using CommunityToolkit.Maui.Alerts;
using VerificaApp.VieModels;

namespace VerificaApp.Views;

public partial class SignUpPage : BasePage
{
    public string Name { get { return "SignUpPage"; } }

    private SignUpViewModel _viewModel;

    public SignUpPage(SignUpViewModel signUpViewModel) : base(signUpViewModel)
	{
		InitializeComponent();

        this._viewModel = signUpViewModel;

        Task.Run(CheckNavigation).Wait();

    }

    private async Task CheckNavigation()
    {
         if (_viewModel.TestForRegisteredUser)
        {
            await _viewModel.TestAlreadySignedUp(Name);
            _viewModel.TestForRegisteredUser = true;
        }
        else
        {
            _viewModel.Login = await SecureStorage.GetAsync("username");
            _viewModel.Phone = await SecureStorage.GetAsync("phone");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

       
    }
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var snackBar = Snackbar.Make(
            CommonConstants.INFO_MOBILE,
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