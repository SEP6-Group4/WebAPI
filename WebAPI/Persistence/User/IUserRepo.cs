namespace WebAPI.Persistence.User
{
    public interface IUserRepo
    {
        Task<Models.User> GetUserAsync(string email);
        Task CreateAccountAsync(Models.User user);
        Task UpdateAccountAsync(Models.User user);
        Task<Models.User> GetUserByID(int id);
    }
}
