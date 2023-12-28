namespace VerificaApp.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private bool testForRegisteredUser;

    [ObservableProperty]
    private VerificaAppUser currentUser;

    #region Methods

    public string VersionString
    {
        get => string.Concat("Versión ", AppInfo.Current.VersionString, ".", AppInfo.Current.BuildString);
    }

    public BaseViewModel()
    {
        TestForRegisteredUser = true;
    }

    /// <summary>
    /// Si el usuario ya se ha registrado redirige a la Login Page, si no al registro
    /// </summary>
    /// <param name="page">Página de donde viene la petición</param>
    public async Task TestAlreadySignedUp(string page)
    {
        
        if (page.Equals("SignUpPage"))
        {
            if (await isUserValidated())
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }
        else
        {
            if (!await isUserValidated())
            {
                await Shell.Current.GoToAsync($"//{nameof(SignUpPage)}");
            }
        }

    }

    public async Task<bool> isUserValidated()
    {
        //Si no se ha validado el usuario 
        return !string.IsNullOrEmpty(await SecureStorage.Default.GetAsync("username")) &&
            !string.IsNullOrEmpty(await SecureStorage.Default.GetAsync("phone")) &&
            !string.IsNullOrEmpty(await SecureStorage.Default.GetAsync("guid"));     
    }

    public async void HandleCredentialsChanged()
    {
        //await SecureStorage.SetAsync("username", "");
        //await SecureStorage.SetAsync("phone", "");
        await SecureStorage.Default.SetAsync("password", "");
        await Shell.Current.DisplayAlert("Ha fallado la validación del usuario. Esto puede deberse a cambios en sus credenciales o a una nueva instalación de la App. La App sólo puede estar instalada en un dispositivo.", "Validación incorrecta", "Aceptar");
        await Shell.Current.GoToAsync(nameof(SignUpPage));
    }

    #endregion
}
