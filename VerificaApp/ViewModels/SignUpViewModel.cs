namespace VerificaApp.ViewModels
{
    public partial class SignUpViewModel: BaseViewModel
    {
        private readonly IVerificaAppService _VerificaAppService;

        #region Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsButtonEnabled))]
        private bool isBusy;
        
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsButtonEnabled))]
        private string phone;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsButtonEnabled))]
        private string login;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsButtonEnabled))]
        private string password;

        [ObservableProperty]
        private bool phoneIsValid;

        public bool IsButtonEnabled
        {
            get => (!String.IsNullOrEmpty(Login) &&
                      !String.IsNullOrEmpty(Phone) &&
                      !String.IsNullOrEmpty(Password) &&
                      !IsBusy);
        }
        
        #endregion

        #region Constructor

        public SignUpViewModel(IVerificaAppService VerificaAppService)
        {
            _VerificaAppService = VerificaAppService;
        }

        
        #endregion

        #region Methods

        /// <summary>
        /// El usuario ha introducido un teléfono, usuario y contraseña, se validan contra el servidor
        /// y si son correctas se guardan en el almacenamiento local para no volver a pedir esta pantalla
        /// y sí la de login.
        /// 
        /// También guarda si el dispositivo tiene posibilidad de biometría.
        /// 
        /// </summary>
        /// <param name="obj"></param>
        [RelayCommand]
        private async Task SignUp(object obj)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                if (String.IsNullOrEmpty(Phone) || String.IsNullOrEmpty(Login) || String.IsNullOrEmpty(Password))
                {
                    throw new Exception("Datos no correctos");
                }

                VerificaAppUser user = new VerificaAppUser
                {
                    phone = Phone,
                    uid = Login.ToLower().TrimEnd(),
                    password = Password
                };
                CurrentUser = user;

                //Envía petición al servidor para validar los datos
                var response = await _VerificaAppService.RegisterUser(user);

                //Respuesta
                if (response == null || !response.code.Equals("OK"))
                {
                    await AppShell.Current.DisplayAlert("Ha ocurrido un error.", CommonConstants.ReturnMessage(response.code.ToString()), "Aceptar");
                }
                else
                {
                    try
                    {

                        user = JsonSerializer.Deserialize(response.content.ToString(), VerificaAppUserContext.Default.VerificaAppUser);
                        CurrentUser.registering = user.registering;
                        if ((bool)user.registering)
                        {
                            MainThreadHelper.BeginInvokeOnMainThread(async () =>
                            {
                                IDictionary<string, object> map = new Dictionary<string, object>();
                                map.Add("user", user);
                                await Shell.Current.GoToAsync($"///{nameof(SmsHandlerPage)}",true,map);
                            });
                        }
                        else
                        {
                            //Si todo ha ido bien se guardan los datos en el almacenamiento local
                            MainThreadHelper.BeginInvokeOnMainThread(async () =>
                            {
                                await Shell.Current.GoToAsync($"///{nameof(ItemsPage)}");
                            });
                        }

                    }
                    catch (Exception ex)
                    {
                        // Possible that device doesn't support secure storage on device.
                        throw;
                    }
                    
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ha ocurrido un error.", CommonConstants.WRONG_VALIDATION, "Aceptar");
            }
            finally 
            {
                IsBusy = false;
            }
        }


        #endregion
    }
}
