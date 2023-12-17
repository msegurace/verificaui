namespace VerificaApp.Utils
{
    public static class CommonConstants
    {
        public static readonly string BASE_URL = "https://evgc001f01-0005.gobcan.net:10500";

        public static readonly string AUTH_USER = "auth";
        public static readonly string VALIDATE_USER_URL = "users/validate";
        public static readonly string REGISTER_USER_URL = "users/register";
        public static readonly string END_REGISTER_USER_URL = "users/endregister";
        public static readonly string GET_TOKENS_URL = "tokens/getallforapp";
        public static readonly string ACCEPT_TOKEN_URL = "tokens/accept";
        public static readonly string REJECT_TOKEN_URL = "tokens/reject";

        public static string TERMINOS_HOME_PHONE_ACEPTADO = "TerminosHomePhoneAceptado";
        //public static string ACCESS_FROM_OTHER_INSTALLATION = "Se ha identificado un intento de acceso a GobCan Verifica desde otro terminal o instalación.Si ha sido usted, siga los pasos que le han indicado para la nueva instalación.. Si no ha sido usted, proceda a actualizar su contraseña lo antes posible desde MiClave.";
        public static string ACCESS_FROM_OTHER_INSTALLATION = "Se ha identificado un intento de acceso a Verifica desde otro terminal o instalación.";
        //public static string INSTALLATION_EXISTS = "Ya existe un terminal con una instalación registrada para estas credenciales. Vaya a MiClave, borre el registro de terminal en la sección ‘Recuperación de contraseñas / GobCan Verifica’ y vuelva a intentarlo.";
        public static string INSTALLATION_EXISTS = "Ya existe un terminal con una instalación registrada para estas credenciales.";
        public static string NO_HOME_PHONE = "Para utilizar GobCan Verifica es necesario que usted registre su móvil en MiClave.";
        public static string WRONG_REGISTRATION = "Teléfono móvil, usuario y/o contraseña incorrectos. Si has cambiado la contraseña vuelve a la página de registro para actualizarla, si no, inténtalo de nuevo.";
        public static string CREDENTIALS_CHANGED = "Ha fallado la validación del usuario. Esto puede deberse a cambios en sus credenciales o a una nueva instalación de la App. La App sólo puede estar instalada en un dispositivo.";
        public static string GUID_NOT_FOUND = "No se ha encontrado una instalación para este dispositivo, por favor, vuelva a registrarla.";
        public static string WRONG_OTP_TITLE = "Error OTP";
        public static string WRONG_OTP = "Validación del código de activación errónea.";
        public static string CODIGO_ACTIVACION = "CODIGO DE ACTIVACION ";
        public static string SERVER_ERROR_TITLE = "Error obteniendo datos desde el servidor";
        public static string ERROR_TITLE = "Ha ocurrido un error";
        public static string INFO_MOBILE = "El teléfono móvil es el que se ha registrado en Gestión de Identidades como método de recuperación";
        public static string INFO_USER_PASSWORD = "Su usuario y contraseña de Gestión de Identidades.";
        public static string BIOMETRIC_TITLE = "Uso de biometría";
        public static string BIOMETRIC_MESSAGE = "Se van a utilizar los datos biométricos registrados en su dispositivo para autenticarle en la aplicación.";

        public static string WRONG_VALIDATION = "Teléfono móvil, Usuario y/o contraseña incorrectas, por favor, inténtalo de nuevo";
        public static string WRONG_BIOMETRIC_VALIDATION = "Error al autenticar mediante biometría.";

        public static string SMS_START = "CODIGO DE ACTIVACION VERIFICA: ";

        //BOTONES
        public static string BUTTON_OK = "OK";
        public static string BUTTON_CANCEL = "CANCEL";
        public static string BUTTON_RETRY = "Reintentar";
        public static string BUTTON_RETURN_TO_SIGNUP = "Volver a la página de registro";
        public static string BUTTON_ACEPTAR = "Aceptar";

        //COLORES

        public static string ReturnMessage(string message)
        {
            switch (message)
            {
                case "ACCESS_FROM_OTHER_INSTALLATION":
                    return TERMINOS_HOME_PHONE_ACEPTADO;
                case "INSTALLATION_EXISTS":
                    return INSTALLATION_EXISTS;
                case "NO_HOME_PHONE":
                    return NO_HOME_PHONE;
                case "WRONG_REGISTRATION":
                    return WRONG_REGISTRATION;
                default:
                    return "";
            }
        }

        //Expiración de sesión en 5 minutos
        public static int SessionExpiration { get { return 10; } }

        //Refresco de búsqueda de autorizaciones pendientes, 1 minuto.
        public static int RefreshTimer { get { return 1; } }


    }
}
