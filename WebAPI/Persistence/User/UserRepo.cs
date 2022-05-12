﻿using Npgsql;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Persistence.User
{
    public class UserRepo : IUserRepo
    {
        private readonly IConfiguration configuration;
        string connectionString;

        public UserRepo(IConfiguration iConfig)
        {
            configuration = iConfig;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public async Task<Models.User> GetUserAsync(string Email)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            string command = $"SELECT * FROM public.\"User\" where \"Email\" = @Email ;";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(command, con))
            {
                cmd.Parameters.AddWithValue("@Email", NpgsqlTypes.NpgsqlDbType.Varchar, Email);

                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        Models.User user = ReadUser(reader);
                        con.Close();
                        return user;
                    }
            }
            con.Close();
            return null;
        }

        private static Models.User ReadUser(NpgsqlDataReader reader)
        {
            try
            {
                Models.User user = new Models.User
                {
                    UserID = reader["UserID"] as int?,
                    FirstName = reader["First Name"] as string,
                    LastName = reader["Last Name"] as string,
                    Birthday = reader["Birthday"] as DateTime?,
                    Email = reader["Email"] as string,
                    Country = reader["Country"] as string,
                    Password = reader["Password"] as string
                };
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task CreateAccountAsync(Models.User user)
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            string command = $"INSERT INTO public.\"User\"(\"First Name\", \"Last Name\", \"Birthday\", \"Email\", \"Country\", \"Password\") VALUES (@FirstName, @LastName, @Birthday, @Email, @Country, @Password);";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(command, con))
            {
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Birthday", user.Birthday);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Country", user.Country);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
    }
}
