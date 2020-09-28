using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Chat.Data.AdoNet
{
    public class UserAdoNetService : IUserService
    {
        private const string loginParameter = "@login";
        private const string passwordParameter = "@password";
        private const string countryParameter = "@country";
        private const string cityParameter = "@city";

        private readonly SqlConnection connection;

        public UserAdoNetService(SqlConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public bool Exists(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException(nameof(login));
            }

            var text = $"SELECT COUNT(*) FROM dbo.Users WHERE Login={loginParameter}";

            using var command = new SqlCommand(text, connection);
            AddLoginParameter(login, command);

            return (int)command.ExecuteScalar() > 0;
        }

        public IEnumerable<User> Get(int offset, int limit)
        {
            if (offset < 0)
            {
                throw new ArgumentException("Must be equal or greater than zero.", nameof(offset));
            }

            if (limit < 1)
            {
                throw new ArgumentException("Must be greater than zero.", nameof(limit));
            }

            var list = new List<User>();

            var text = "SELECT Id, Login, Password, Country, City FROM dbo.Users " +
                $"ORDER BY Id OFFSET {offset} ROWS FETCH FIRST {limit} ROWS ONLY";

            using var command = new SqlCommand(text, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(ReadUser(reader));
            }

            return list;
        }

        public void Create(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var text = "INSERT INTO dbo.Users (Login, Password, Country, City) " +
                $"VALUES ({loginParameter}, {passwordParameter}, {countryParameter}, {cityParameter})";

            using var command = new SqlCommand(text, connection);
            AddUserParameters(user, command);

            command.ExecuteNonQuery();
        }

        public User Read(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException(nameof(login));
            }

            var text = $"SELECT Id, Login, Password, Country, City FROM dbo.Users WHERE Login={loginParameter}";

            using var command = new SqlCommand(text, connection);
            AddLoginParameter(login, command);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                throw new UserNotFoundException();
            }

            return ReadUser(reader);
        }

        public void Update(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var text = "UPDATE dbo.Users SET " +
                $"Password={passwordParameter}, Country={countryParameter}, City={cityParameter} " +
                $"WHERE Login={loginParameter} SELECT @@ROWCOUNT";

            using var command = new SqlCommand(text, connection);
            AddUserParameters(user, command);

            if ((int)command.ExecuteScalar() == 0)
            {
                throw new UserNotFoundException();
            }
        }

        public void Delete(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException(nameof(login));
            }

            var text = $"DELETE FROM dbo.Users WHERE Login={loginParameter} SELECT @@ROWCOUNT";

            using var command = new SqlCommand(text, connection);
            AddLoginParameter(login, command);

            if ((int)command.ExecuteScalar() == 0)
            {
                throw new UserNotFoundException();
            }
        }

        private void AddUserParameters(User user, SqlCommand command)
        {
            AddLoginParameter(user.Login, command);
            AddPasswordParameter(user.Password, command);
            AddCountryParameter(user.Country, command);
            AddCityParameter(user.City, command);
        }

        private void AddLoginParameter(string login, SqlCommand command)
        {
            command.Parameters.Add(loginParameter, SqlDbType.NVarChar, 50);
            command.Parameters[loginParameter].Value = login;
        }

        private void AddPasswordParameter(string password, SqlCommand command)
        {
            command.Parameters.Add(passwordParameter, SqlDbType.NVarChar, 50);
            command.Parameters[passwordParameter].Value = password;
        }

        private void AddCountryParameter(string country, SqlCommand command)
        {
            command.Parameters.Add(countryParameter, SqlDbType.NVarChar, 50);
            command.Parameters[countryParameter].IsNullable = true;
            command.Parameters[countryParameter].Value = country is null ? DBNull.Value : country as object;
        }

        private void AddCityParameter(string city, SqlCommand command)
        {
            command.Parameters.Add(cityParameter, SqlDbType.NVarChar, 50);
            command.Parameters[cityParameter].IsNullable = true;
            command.Parameters[cityParameter].Value = city is null ? DBNull.Value : city as object;
        }

        private User ReadUser(SqlDataReader reader)
        {
            var id = (int)reader["Id"];
            var login = (string)reader["Login"];
            var password = (string)reader["Password"];
            var country = reader["Country"] == DBNull.Value ? null : (string)reader["Country"];
            var city = reader["City"] == DBNull.Value ? null : (string)reader["City"];

            return new User()
            {
                Id = id,
                Login = login,
                Password = password,
                Country = country,
                City = city
            };
        }
    }
}
