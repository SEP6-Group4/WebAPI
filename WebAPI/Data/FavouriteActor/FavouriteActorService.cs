using WebAPI.Data.Actors;
using WebAPI.Models;
using WebAPI.Persistence.FavouriteActor;
using WebAPI.Persistence.User;

namespace WebAPI.Data.FavouriteActor
{
    public class FavouriteActorService : IFavouriteActorService
    {
        IFavouriteActorRepo repo;
        IUserRepo userRepo;
        IActorService actorService;

        public FavouriteActorService(IConfiguration configuration)
        {
            repo = new FavouriteActorRepo(configuration);
            userRepo = new UserRepo(configuration);
            actorService = new ActorService(configuration);
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

        public async Task<List<Actor>> GetFavouriteActorIdsByEmail(string email)
        {
            Models.User user = await userRepo.GetUserAsync(email);

            if (user.FavouritePrivacy == true) return null;

            List<int> actorIds = await repo.GetFavouriteActorIds((int)user.UserID);
            List<Actor> actors = new List<Actor>();
            foreach (var actorId in actorIds)
            {
                Actor actor = await actorService.GetActorByID(actorId);
                if(actor == null) continue;
                actors.Add(actor);
            }
            return actors;
        }
    }
}
