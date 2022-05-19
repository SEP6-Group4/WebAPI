using Npgsql;

namespace WebAPI.Persistence.FavouriteActor
{
    public class FavouriteActorRepo : IFavouriteActorRepo
    {
        private readonly IConfiguration configuration;
        string connectionString;

        public FavouriteActorRepo(IConfiguration iConfig)
        {
            configuration = iConfig;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public async Task AddActorToFavourite(int userId, int actorId)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            string command = $"INSERT INTO public.\"FavouriteActor\"(\"UserID\", \"ActorID\")VALUES (@userId, @actorId);";

            await using (NpgsqlCommand cmd = new NpgsqlCommand(command, con))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@actorId", actorId);

                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public async Task RemoveActorFromFavourite(int userId, int actorId)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            string command = $"DELETE FROM public.\"FavouriteActor\" WHERE (\"UserID\", \"ActorID\") = (@userId, @actorId);";

            await using (NpgsqlCommand cmd = new NpgsqlCommand(command, con))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@actorId", actorId);

                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public async Task<List<int>> GetFavouriteActorIds(int userId)
        {
            List<int> actorIds = new List<int>();

            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            string command = $"SELECT \"ActorID\" from public.\"FavouriteActor\" WHERE \"UserID\" = @userId;";



            await using (NpgsqlCommand cmd = new NpgsqlCommand(command, con))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        int? idToAdd = reader["ActorID"] as int?;
                        if (idToAdd != null) actorIds.Add((int)idToAdd);
                    }
                con.Close();
                return actorIds;
            }

        }
    }
}
