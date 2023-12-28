namespace VerificaApp.Services
{
    public interface IVerificaAppService
    {
        Task<VerificaAppGenericResponse> GenericRequest(VerificaAppUser user, string endpoint);

        Task<VerificaAppGenericResponse> RegisterUser(VerificaAppUser user);
        Task<VerificaAppGenericResponse> EndRegisterUser(VerificaAppUser user);

        Task<VerificaAppGenericResponse> ValidateUser(VerificaAppUser user);

        Task<List<ItemDto>> GetTokens(int idUser);

        Task<bool> AcceptToken(int id);

        Task<bool> RejectToken(int id);

    }
}
