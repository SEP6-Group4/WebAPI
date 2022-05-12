using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using WebAPI.Models;
using WebAPI.Models.Encryption;
using WebAPI.Persistence.User;

namespace WebAPI.Data.User
{
    public class UserService : IUserService
    {
        IUserRepo repo;

        public UserService()
        {
            repo = new UserRepo();
        }

        public async Task<string> GetEncryptedPassword(string password)
        {
            return Encrypt.EncryptString(password);
        }

        public async Task<Models.User> ValidateUser(Models.User user)
        {
            user.Password = Encrypt.EncryptString(user.Password);

            Models.User verifiedUser = await VerifyUser(user);

            if (verifiedUser == null)
            {
                return null;
            }
            else
            {
                return verifiedUser;
            }
        }

        private async Task<Models.User> VerifyUser(Models.User user)
        {
            Models.User userToVerify = await repo.GetUserAsync(user.Email);

            if(userToVerify == null)
            {
                return null;
            }

            if(user.Password == null)
            {
                return null;
            }

            if(user.Password == userToVerify.Password)
            {
                return userToVerify;
            }

            return null;
        }
    }    
}
