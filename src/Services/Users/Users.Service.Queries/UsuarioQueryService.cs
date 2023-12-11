using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System.Data;
using User.Service.Queries.DTOs;
using UserDomain;
using Users.Persistence.Database;
using Users.Service.Queries.DTOs;

namespace Users.Service.Queries
{
    public interface IUsuarioQueryService
    {
        Task<DataCollection<UsuarioDto>> GetAllAsync(int page, int take, IEnumerable<int> users = null);
        Task<UsuarioDto> GetAsync(int id);
        Task<UsuarioDto> LoginAsync(LoginInformation info);
        Task<VerificaGenericResponse> RegisterAsync(VerificaAppUserDto user);
        Task<VerificaGenericResponse> EndRegistrationAsync(VerificaAppUserDto user);
    }

    public class UsuarioQueryService: IUsuarioQueryService
    {
        private ApplicationDbContext _context;
        
        public UsuarioQueryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCollection<UsuarioDto>> GetAllAsync(int page, int take, IEnumerable<int> users = null)
        {
            var collection = await _context.Usuarios.Where(x => users == null || users.Contains(x.id))
                .OrderBy(x => x.id)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<UsuarioDto>>();
        }

        public async Task<UsuarioDto> GetAsync(int id)
        {
            return (await _context.Usuarios.SingleAsync(x => x.id == id)).MapTo<UsuarioDto>();
        }

        public async Task<UsuarioDto> LoginAsync(LoginInformation info)
        {
            try
            {

                var user = (await _context.Usuarios.SingleAsync(x => x.username.ToLower().Trim().Equals(info.username.ToLower().Trim())
                                    && x.password.Equals(info.password))).MapTo<UsuarioDto>();
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<VerificaGenericResponse> RegisterAsync(VerificaAppUserDto info)
        {
            var user = new UsuarioDto();
            var resp = new VerificaGenericResponse();
            resp.code = "OK";
            try
            {

                user = (await _context.Usuarios.SingleAsync(x => x.username.ToLower().Trim().Equals(info.uid.ToLower().Trim())
                                    && x.password.Equals(info.password))).MapTo<UsuarioDto>();
                
                if (user != null)
                {
                    if (user.telefono.Equals(info.phone))
                    {
                        if (user.guid.Equals(Guid.Empty))
                        {
                            info.registering = true;
                            await HandleSMS(user.username);
                        }
                        else
                        {
                            resp.code = "INSTALLATION_EXISTS";
                        }
                    }
                    else
                    {
                        resp.code = "NO_HOME_PHONE";
                    }
                }
                else
                {
                    resp.code = "WRONG_REGISTRATION";
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (resp.code.Equals("OK"))
            {
                resp.content = info;
            }

            return resp;
        }

        /// <summary>
        /// Verifica que la OTP y el usuario recibidos coincidan con los que están en la base de datos.
        /// Si es correcto registrará el guid del usuario en GdI y finalizará así el registro.
        /// </summary>
        /// <param name="user_sms">usuario y otp a comprobar</param>
        /// <returns>true si es correcto, false si no</returns>
        public async Task<VerificaGenericResponse> EndRegistrationAsync(VerificaAppUserDto user_sms)
        {
            VerificaGenericResponse resp = new VerificaGenericResponse();
            resp.code = "OK";
            if (_context.UserSMS.Where(r => r.uid.Trim().ToLower().Equals(user_sms.uid.Trim().ToLower()) &&
                    r.otp.Trim().Equals(user_sms.otp!.Trim()) &&
                    r.expiration_date >= DateTime.UtcNow).Any())
            {
                var user = await _context.Usuarios.SingleAsync(x => x.username == user_sms.uid);
                if (user.guid.Equals(Guid.Empty))
                {
                    var guid = Guid.NewGuid();
                    user_sms.otp = String.Empty;
                    user_sms.guid = guid;
                    user.guid = guid;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    resp.content = user_sms;
                }
                else
                {
                    resp.code = "INSTALLATION_EXISTS";
                }
            }
            else
            {
                resp.code = "WRONG_REGISTRATION";
            }
            return resp;
        }


        /// <summary>
        /// Si no tiene GUID y no ha validado la OTP, la genero para enviarla
        /// </summary>
        /// <param name="uid"></param>
        private async Task<string> HandleSMS(string uid)
        {
            var code = String.Empty;
            try
            {
                //Envío OTP
                code = GenerateRandomOTP();
              
                await SaveCode(uid, code);

                //  await _smsHandler.SendSMSFromGDI(home_phone, code);
            }
            catch (System.Exception)
            {

                throw;
            }
            return code;
        }

        private string GenerateRandomOTP()
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

            int iOTPLength = 6;

            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }

        /// <summary>
        /// Guarda el código OTP en la base datos.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private Task<bool> SaveCode(string uid, string code)
        {
            try
            {
                VerificaUserSMS gcv = new VerificaUserSMS()
                {
                    uid = uid,
                    otp = code,
                    expiration_date = DateTime.UtcNow.AddMinutes(5)
                };

                _context.Add(gcv);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
