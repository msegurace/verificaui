namespace VerificaApp.Services
{
    public interface IVerificaAppService
    {
        Task<VerificaAppGenericResponse> GenericRequest(VerificaAppUser user, string endpoint);

        Task<VerificaAppGenericResponse> RegisterUser(VerificaAppUser user);
        Task<VerificaAppGenericResponse> EndRegisterUser(VerificaAppUser user);

        Task<VerificaAppGenericResponse> ValidateUser(VerificaAppUser user);

        //Task<List<AuthRequest>> GetTokens(VerificaAppUser user);      
        Task<VerificaAppGenericResponse> GetTokens(VerificaAppUser user);

        Task<string> AcceptToken(VerificaAppUser user);

        Task<string> RejectToken(VerificaAppUser user);

    }
}
