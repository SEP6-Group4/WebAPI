using WebAPI.Models;

namespace WebAPI.Data.Actors
{
    public interface IActorService
    {
        Task<Actor> GetActorByID(int id);
        Task<MovieCredit> GetMovieCreditsByActorId(int actorId);
    }
}
