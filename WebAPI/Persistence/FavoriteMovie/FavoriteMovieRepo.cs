using Npgsql;
using WebAPI.Models;

namespace WebAPI.Persistence.FavoriteMovie
{
    public class FavoriteMovieRepo : IFavoriteMovieRepo
    {
        private readonly IConfiguration configuration;
        string connectionString;

        public FavoriteMovieRepo(IConfiguration iConfig)
        {
            configuration = iConfig;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        /// <summary>
        /// Adds the provided movie to the provided user's favorite movies
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="movieID"></param>
        /// <returns></returns>
        public async Task AddFavoriteMovie(int userID, int movieID)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            string command = $"INSERT INTO public.\"FavouriteMovie\"(\"UserID\", \"MovieID\") VALUES (@userID, @movieID);";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(command, con))
            {
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@movieID", movieID);

                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        /// <summary>
        /// Returns a list of movie IDs that the specified userID has marked as favorite
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<int>> GetFavoriteMoviesByID(int userID)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            List<int> movieIDs = new List<int>();

            string command = $"SELECT * FROM public.\"FavouriteMovie\" where \"UserID\" = @UserID ;";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(command, con))
            {
                cmd.Parameters.AddWithValue("@UserID", NpgsqlTypes.NpgsqlDbType.Integer, userID);

                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        int? idToAdd = reader["MovieID"] as int?;
                        if (idToAdd != null) movieIDs.Add((int)idToAdd);
                    }
                return movieIDs;
            }
            con.Close();
            return null;

        }

        /// <summary>
        /// Returns true or false depending if movie is marked as favorite by provided user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="movieID"></param>
        /// <returns></returns>
        public async Task<bool> GetIsFavoriteMovieByID(int userID, int movieID)
        {
            List<int> movieIDs = await GetFavoriteMoviesByID(userID);

            foreach (var movie in movieIDs)
            {
                if(movie == movieID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes provided favorite movie from the provided user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="movieID"></param>
        /// <returns></returns>
        public async Task RemoveFavoriteMovieByID(int userID, int movieID)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            string command = $"DELETE FROM public.\"FavouriteMovie\" where \"UserID\" = @userID AND \"MovieID\" = @movieID ;";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(command, con))
            {
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@movieID", movieID);

                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
    }
}
