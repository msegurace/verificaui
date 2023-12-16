using CommunityToolkit.Mvvm.Input;
using VerificaApp.Models;
using Plugin.Fingerprint;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace VerificaApp.ViewModels
{
    public partial class SmsHandlerViewModel : BaseViewModel
    {
        private readonly IVerificaAppService _VerificaAppService;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string smsActivationCode;

        [ObservableProperty]
        public bool isSMSButtonEnabled;

        public SmsHandlerViewModel(IVerificaAppService VerificaAppService)
        {
            _VerificaAppService = VerificaAppService;

            SmsActivationCode = "";
            IsSMSButtonEnabled = false;
        }

        /// <summary>
        /// El usuario ha introducido un teléfono, usuario y contraseña, se validan contra el servidor
        /// y si son correctas se guardan en el almacenamiento local para no volver a pedir esta pantalla
        /// y sí la de login.
        /// 
        /// También guarda si el dispositivo tiene posibilidad de biometría.
        /// 
        /// </summary>
        private async Task SMSButton()
        {
            MainThreadHelper.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
            });
        }

        /// <summary>
        /// Valida el código OTP en el servidor
        /// </summary>
        [RelayCommand]
        private async Task ValidateOtp(object obj)
        {
            try
            {
                IsSMSButtonEnabled = false;
                IsBusy = true;

                //Envío sólo los datos necesarios
                VerificaAppUser u = new VerificaAppUser()
                {
                    uid = CurrentUser.uid,
                    otp = SmsActivationCode
                };

                //Envía petición al servidor para validar los datos
                var response = await _VerificaAppService.EndRegisterUser(u);
                System.Diagnostics.Debug.WriteLine($"OnValidateOTP {response}");
                if (!response.code.Equals("OK"))
                {
                    await AppShell.Current.DisplayAlert(CommonConstants.WRONG_OTP_TITLE, CommonConstants.WRONG_OTP + " - " + CommonConstants.ReturnMessage(response.code), "Aceptar");
                }
                else
                {
                    var user = JsonSerializer.Deserialize(response.content.ToString(), VerificaAppUserContext.Default.VerificaAppUser);
                    CurrentUser.guid = user.guid;
                    await SaveStorageData();
                    MainThreadHelper.BeginInvokeOnMainThread(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
                    });
                }
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException ||
                    ex.Message.Equals("timeout"))
                {
                    await AppShell.Current.DisplayAlert("Error obteniendo datos desde el servidor", ex.Message, "Aceptar");
                }
                else
                {
                    await AppShell.Current.DisplayAlert(CommonConstants.WRONG_OTP_TITLE, CommonConstants.WRONG_OTP + ex.Message, "Aceptar");
                }
            }
            IsBusy = false;
        }

        private async Task SaveStorageData()
        {
            await SecureStorage.SetAsync("phone", CurrentUser.phone);
            await SecureStorage.SetAsync("username", CurrentUser.uid);
            await SecureStorage.SetAsync("password", CurrentUser.password);
            await SecureStorage.SetAsync("biometricenabled", (await CrossFingerprint.Current.IsAvailableAsync()).ToString());
            await SecureStorage.SetAsync("guid", CurrentUser.guid.ToString());
        }
    }

}
