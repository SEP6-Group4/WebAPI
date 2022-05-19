using WebAPI.Persistence.FavouriteActor;

namespace WebAPI.Data.FavouriteActor
{
    public class FavouriteActorService : IFavouriteActorService
    {
        IFavouriteActorRepo repo;

        public FavouriteActorService(IConfiguration configuration)
        {
            repo = new FavouriteActorRepo(configuration);
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
    }
}
