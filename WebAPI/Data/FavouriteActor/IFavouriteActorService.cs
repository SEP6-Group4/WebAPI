namespace WebAPI.Data.FavouriteActor
{
    public interface IFavouriteActorService
    {
        Task AddActorToFavourite(int userId, int actorId);

        Task RemoveActorFromFavourite(int userId, int actorId);

        Task<List<int>> GetFavouriteActorIds(int userId);
    }
}
