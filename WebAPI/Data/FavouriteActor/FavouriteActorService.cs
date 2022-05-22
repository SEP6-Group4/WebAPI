using WebAPI.Persistence.FavouriteActor;
using WebAPI.Persistence.User;

namespace WebAPI.Data.FavouriteActor
{
    public class FavouriteActorService : IFavouriteActorService
    {
        IFavouriteActorRepo repo;
        IUserRepo userRepo;

        public FavouriteActorService(IConfiguration configuration)
        {
            repo = new FavouriteActorRepo(configuration);
            userRepo = new UserRepo(configuration);
        }

        public async Task AddActorToFavourite(int userId, int actorId)
        {
            await repo.AddActorToFavourite(userId, actorId);
        }

        public async Task RemoveActorFromFavourite(int userId, int actorId)
        {
            await repo.RemoveActorFromFavourite(userId, actorId);
        }

        public async Task<List<int>> GetFavouriteActorIds(int userId)
        {
            List<int> actorIds = await repo.GetFavouriteActorIds(userId);
            return actorIds;
        }

        public async Task<List<int>> GetFavouriteActorIdsByEmail(string email)
        {
            Models.User user = await userRepo.GetUserAsync(email);
            return await repo.GetFavouriteActorIds((int)user.UserID);
        }
    }
}
