using Npgsql;

namespace WebAPI.Persistence.User
{
    public class UserRepo : IUserRepo
    {
        string connectionString = "Host=sep6.c7szkct1z4j9.us-east-1.rds.amazonaws.com;Username=sep6;Password=dingdong69420!;Database=sep6";

        public async Task<Models.User> GetUserAsync(string Email)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            string command = $"SELECT * FROM public.\"User\" where \"Email\" = 'sep@sep.sep';";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(command, con))
            {
                cmd.Parameters.AddWithValue("Email", NpgsqlTypes.NpgsqlDbType.Varchar, Email);

                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        Models.User user = ReadUser(reader);
                        return user;
                    }
            }

            return null;
        }

        private static Models.User ReadUser(NpgsqlDataReader reader)
        {
            Models.User user = new Models.User
            {
                UserID = reader["UserID"] as int?,
                FirstName = reader["First Name"] as string,
                LastName = reader["Last Name"] as string,
                Birthday = reader["Birthday"] as DateOnly?,
                Email = reader["Email"] as string,
                Country = reader["Country"] as string,
                Password = reader["Password"] as string
            };

            return user;
        }
    }
}
