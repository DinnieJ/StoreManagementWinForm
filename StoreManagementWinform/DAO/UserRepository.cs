using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoreManagementWinform.Model;
using System.Data.SqlClient;

namespace StoreManagementWinform.DAO
{
    class UserRepository : BaseRepository
    {
        public List<User> getAllUser()
        {
            return Context.ExecuteQuery<List<User>>(con =>
            {
                SqlCommand command = new SqlCommand(null, con);

                command.CommandText =
                    "SELECT * FROM [User]";
                var reader = command.ExecuteReader();
                List<User> result = new List<User>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new User()
                        {
                            ID = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                            Name = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            DateOfBirth = reader.GetDateTime(5),
                            Role = reader.GetString(6),
                            CreatedAt = reader.GetDateTime(7),
                            UpdatedAt = reader.GetDateTime(8)
                        });
                    }
                }

                return result;
            });
        }

        public User GetUserByCreds(string username, string password)
        {
            return Context.ExecuteQuery<User>(conn =>
            {
                SqlCommand command = new SqlCommand(null, conn)
                {
                    CommandText = "SELECT TOP 1 * FROM [User] WHERE [User].[Username] = @username and [User].[Password] = @password"
                };

                command.Parameters.Add(new SqlParameter("@username", System.Data.SqlDbType.NVarChar) { Value = username });
                command.Parameters.Add(new SqlParameter("@password", System.Data.SqlDbType.NVarChar) { Value = password });

                var reader = command.ExecuteReader();
                User user = null;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user = new User()
                        {
                            ID = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                            Name = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            DateOfBirth = reader.GetDateTime(5),
                            Role = reader.GetString(6),
                            CreatedAt = reader.GetDateTime(7),
                            UpdatedAt = reader.GetDateTime(8)
                        };
                    }
                }
                return user;
            });
        }

        public void ChangePassword(User u, string newPassword)
        {
            Context.ExecuteUpdate((conn) =>
            {
                SqlCommand command = new SqlCommand(null, conn)
                {
                    CommandText =
                        "UPDATE [User] SET [User].[Password] = @newPassword WHERE [User].ID = @id"
                };

                command.Parameters.Add(new SqlParameter("@newPassword", System.Data.SqlDbType.NVarChar) { Value = newPassword });
                command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = u.ID });

                command.ExecuteNonQuery();
            });
        }

        public User AddUser(User u)
        {
            return Context.ExecuteQuery<User>((conn) =>
            {
                SqlCommand command = new SqlCommand(null, conn) {
                    CommandText =
                        "INSERT INTO [User]([Name],[Username],[Password],[PhoneNumber],[DateOfBirth],[Role]) VALUES \n" +
                        "(@name,@username,@password,@phonenum,@dateofbirth,@role)\n" +
                        "SELECT * FROM [User] WHERE ID = SCOPE_IDENTITY()"
                };

                command.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.NVarChar) { Value = u.Name });
                command.Parameters.Add(new SqlParameter("@username", System.Data.SqlDbType.NVarChar) { Value = u.Username });
                command.Parameters.Add(new SqlParameter("@password", System.Data.SqlDbType.NVarChar) { Value = u.Password });
                command.Parameters.Add(new SqlParameter("@phonenum", System.Data.SqlDbType.NVarChar) { Value = u.PhoneNumber });
                command.Parameters.Add(new SqlParameter("@dateofbirth", System.Data.SqlDbType.NVarChar) { Value = u.DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss.fff") });
                command.Parameters.Add(new SqlParameter("@role", System.Data.SqlDbType.NVarChar) { Value = u.Role });

                var reader = command.ExecuteReader();
                User user = null;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user = new User()
                        {
                            ID = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                            Name = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            DateOfBirth = reader.GetDateTime(5),
                            Role = reader.GetString(6),
                            CreatedAt = reader.GetDateTime(7),
                            UpdatedAt = reader.GetDateTime(8)
                        };
                    }
                }
                return user;
            });
        }

        public void UpdateUser(User u)
        {
            Context.ExecuteUpdate((conn) =>
            {
                SqlCommand command = new SqlCommand(null, conn)
                {
                    CommandText =
                        "UPDATE [User]\n" +
                        "SET\n" +
                        "[User].[Name] = @name," +
                        "[User].[Username] = @username, " +
                        "[User].[Password] = @password, " +
                        "[User].[PhoneNumber] = @phonenum, " +
                        "[User].[DateOfBirth] = @dateofbirth, " +
                        "[User].[Role] = @role " +
                        "WHERE [User].[ID] = @id"
                };

                command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = u.ID });
                command.Parameters.Add(new SqlParameter("@name", System.Data.SqlDbType.NVarChar) { Value = u.Name });
                command.Parameters.Add(new SqlParameter("@username", System.Data.SqlDbType.NVarChar) { Value = u.Username });
                command.Parameters.Add(new SqlParameter("@password", System.Data.SqlDbType.NVarChar) { Value = u.Password });
                command.Parameters.Add(new SqlParameter("@phonenum", System.Data.SqlDbType.NVarChar) { Value = u.PhoneNumber });
                command.Parameters.Add(new SqlParameter("@dateofbirth", System.Data.SqlDbType.NVarChar) { Value = u.DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss.fff") });
                command.Parameters.Add(new SqlParameter("@role", System.Data.SqlDbType.NVarChar) { Value = u.Role });

                command.ExecuteNonQuery();
            });
        }

        public void DeleteUser(int id)
        {
            Context.ExecuteUpdate(conn =>
            {
                SqlCommand command = new SqlCommand(null, conn)
                {
                    CommandText = "DELETE FROM [User] WHERE [User].[ID] = @id"
                };

                command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = id });

                command.ExecuteNonQuery();
            });
        }
    }
}
