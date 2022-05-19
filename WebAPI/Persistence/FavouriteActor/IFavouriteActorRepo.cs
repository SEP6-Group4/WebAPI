namespace WebAPI.Persistence.FavouriteActor
{
    public interface IFavouriteActorRepo
    {
        Task AddActorToFavourite(int userId, int actorId);

        Task RemoveActorFromFavourite(int userId, int actorId);

        Task<List<int>> GetFavouriteActorIds(int userId);
    }
}
