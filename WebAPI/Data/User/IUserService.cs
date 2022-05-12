namespace WebAPI.Data.User
{
    public interface IUserService
    {
        Task<Models.User> ValidateUser(Models.User user);

        Task<string> GetEncryptedPassword(string password);

        Task CreateAccount(Models.User user);
    }
}
