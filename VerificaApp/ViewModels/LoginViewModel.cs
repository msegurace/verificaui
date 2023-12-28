using CommunityToolkit.Mvvm.Input;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;

namespace VerificaApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IVerificaAppService _VerificaAppService;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsButtonEnabled))]
        private string login;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsButtonEnabled))]
        private string password;

        [ObservableProperty]
        private bool isBiometricsEnabled;

        [ObservableProperty]
        private bool isButtonRegisterVisible;


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsButtonEnabled))]
        [NotifyPropertyChangedFor(nameof(IsBiometricsEnabled))]
        [NotifyPropertyChangedFor(nameof(IsButtonRegisterVisible))]
        bool isBusy = false;
        
        public bool IsButtonEnabled
        {
            get => !String.IsNullOrEmpty(Login) &&
                  !String.IsNullOrEmpty(Password) &&
                  !IsBusy;
        }

        
        
        #region Constructor
        
        public LoginViewModel(IVerificaAppService VerificaAppService)
        {
            _VerificaAppService = VerificaAppService;

            IsButtonRegisterVisible = true;

            Login = SecureStorage.GetAsync("username").Result;
        }
        #endregion

        /// <summary>
        /// Acción de validar al usuario
        /// 
        /// Se validará al usuairo con el móvil guardado en el registro, usr y pwd. 
        /// Si no coinciden no entrará.
        /// 
        /// </summary>
        [RelayCommand]
        private async Task PerformLogin(Object obj)
        {
            if (String.IsNullOrEmpty(Login) || String.IsNullOrEmpty(Password))
            {
                return;   
            }
            else
            {
                IsBusy = true;

                if (await validateUser(Login, Password))
                {
                    await SecureStorage.SetAsync("username", Login);
                    await SecureStorage.SetAsync("password", Password);
                    MainThreadHelper.BeginInvokeOnMainThread(async () =>
                    {
                        //Redirecciona a autorizaciones
                        IDictionary<string, object> map = new Dictionary<string, object>();
                        map.Add("user", CurrentUser);
                        await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}",true, map);
                    });                    
                }
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task FingerPrint()
        {
            var authResult = await CrossFingerprint.Current.AuthenticateAsync(
                            new AuthenticationRequestConfiguration(CommonConstants.BIOMETRIC_TITLE,
                                CommonConstants.BIOMETRIC_MESSAGE));
            //No se ha mostrado la validación
            if (authResult.Status.Equals(FingerprintAuthenticationResultStatus.UnknownError) ||
                (authResult.Status.Equals(FingerprintAuthenticationResultStatus.Canceled)) ||
                (authResult.Status.Equals(FingerprintAuthenticationResultStatus.NotAvailable)) ||
                (authResult.Status.Equals(FingerprintAuthenticationResultStatus.Denied)))
            {
                return;
            }
            if (!authResult.Authenticated)
            {
                await AppShell.Current.DisplayAlert("Error", "Error al autenticar mediante biometría.", "OK");
            }
            else
            {
                IsBusy = true;
                //Compruebo que tiene usuario y contraseña guardado y que son correctos
                if (!String.IsNullOrEmpty(SecureStorage.GetAsync("username").Result) &&
                    !String.IsNullOrEmpty(SecureStorage.GetAsync("password").Result) &&
                    await validateUser(
                        SecureStorage.GetAsync("username").Result,
                        SecureStorage.GetAsync("password").Result))
                {
                    
                        MainThreadHelper.BeginInvokeOnMainThread(async () =>
                        {
                            //Redirecciona a autorizaciones
                            IDictionary<string, object> map = new Dictionary<string, object>();
                            map.Add("user", CurrentUser);
                            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}",true, map);
                        });
                    
                } 
                IsBusy = false;
            }
        }

        private async Task<bool> validateUser(string Login, string Password)
        {
            try
            {
                Guid guid = Guid.Empty;
                if (!string.IsNullOrEmpty(await SecureStorage.GetAsync("guid")))
                {
                    guid = Guid.Parse(SecureStorage.GetAsync("guid").Result);
                }
                else
                {
                    MainThreadHelper.BeginInvokeOnMainThread(async () =>
                    {
                        VerificaAppUser smsuser = new VerificaAppUser
                        {
                            phone = await SecureStorage.GetAsync("phone"),
                            uid = Login,
                            password = Password,
                        };
                        IDictionary<string, object> map = new Dictionary<string, object>();
                        map.Add("user", smsuser);
                        await Shell.Current.GoToAsync($"///{nameof(SmsHandlerPage)}", true, map);
                    });
                    return false;
                }

                VerificaAppUser user = new VerificaAppUser
                {
                    phone = await SecureStorage.GetAsync("phone"),
                    uid = Login,
                    password = Password,
                    guid = guid
                };
                var response = await _VerificaAppService.ValidateUser(user);
                if (response.code.Equals("OK"))
                {
                    user = JsonSerializer.Deserialize(response.content.ToString(), VerificaAppUserContext.Default.VerificaAppUser);
                    CurrentUser = user;
                    return true;
                }
                else
                {
                    throw new Exception(CommonConstants.ReturnMessage(response.code.ToString()));
                }                
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert(CommonConstants.ERROR_TITLE, ex.Message, "Aceptar");

                return false;
            }
        }

        //Comprueba si el teléfono tiene sensor de huellas y si ya hay un usuario validado
        public async Task BiometricsAvailable()
        {

            //Console.WriteLine("CrossFingerprint: " + CrossFingerprint.Current.ToString());
            //Console.WriteLine("CrossFingerprint: " + await CrossFingerprint.Current.IsAvailableAsync());
            //Console.WriteLine("Is User Validated: " + await isUserValidated());
            //Console.WriteLine("biometricenabled: " + bool.Parse(await SecureStorage.GetAsync("biometricenabled")));
            //var avialable = await CrossFingerprint.Current.GetAvailabilityAsync();
            IsBiometricsEnabled =
                await CrossFingerprint.Current.IsAvailableAsync() &&
                await isUserValidated() &&
                await SecureStorage.GetAsync("biometricenabled") != null &&
                bool.Parse(await SecureStorage.GetAsync("biometricenabled"));
        }

        [RelayCommand]
        private async Task Register(Object obj)
        {
            MainThreadHelper.BeginInvokeOnMainThread(async () =>
            {
                TestForRegisteredUser = false;
                await Shell.Current.GoToAsync($"///{nameof(SignUpPage)}");
            });

        }

    }
}
