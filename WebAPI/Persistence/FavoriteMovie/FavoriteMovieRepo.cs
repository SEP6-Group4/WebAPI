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
            if (await GetIsFavoriteMovieByID(userID, movieID)) return; //if the movie is already marked as favorite, just return

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
                if (movie == movieID)
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
            if (await GetIsFavoriteMovieByID(userID, movieID) == false) return; //if the movie is not a favorite, just return

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

        public async Task<int> GetFavoriteMovieCount(int movieID)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();
            int count = 0;

            string command = $"SELECT COUNT(\"MovieID\") FROM public.\"FavouriteMovie\" WHERE \"MovieID\" = @movieID;";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(command, con))
            {
                cmd.Parameters.AddWithValue("@movieID", movieID);

                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        count = Int32.Parse(reader["count"].ToString());
                    }
            }
            con.Close();
            return count;
        }

        public async Task<List<IdCount>> GetFavoriteMoviesByAgeGroup(int ageGroup)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();
            List<IdCount> movieIDs = new List<IdCount>();

            string command = $"SELECT \"MovieID\", COUNT(\"MovieID\") FROM public.\"FavouriteMovie\" WHERE \"UserID\" IN (SELECT \"UserID\" FROM public.\"User\" WHERE \"AgeGroup\" = @ageGroup) GROUP BY \"MovieID\" ORDER BY count DESC LIMIT 5";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(command, con))
            {
                cmd.Parameters.AddWithValue("@ageGroup", ageGroup);

                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        int? idToAdd = reader["MovieID"] as int?;
                        long? countToAdd = reader["count"] as long?;
                        if (idToAdd != null && countToAdd != null) movieIDs.Add(new IdCount {MovieId = (int)idToAdd, count = (int)countToAdd});
                    }
                return movieIDs;
            }
            con.Close();
            return null;
        }
    }
}
