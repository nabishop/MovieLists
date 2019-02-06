using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MovieBackend.Models
{
    public class UserContext : DbContext
    {
        public string ConnectionString { get; set; }

        public DbSet<UserItem> UserItems { get; set; }

        public UserContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<UserItem> GetUsers()
        {
            List<UserItem> list = new List<UserItem>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from user", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new UserItem()
                        {
                            Id = Convert.ToInt32(reader["user_id"]),
                            Name = reader["user_name"].ToString(),
                            Password = reader["user_password"].ToString(),
                        });
                    }
                }
            }
            return list;

        }

        public UserItem GetUserWithId(int id)
        {
            UserItem user;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user WHERE user_id=" + id, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new UserItem()
                        {
                            Id = Convert.ToInt32(reader["user_id"]),
                            Name = reader["user_name"].ToString(),
                            Password = reader["user_password"].ToString()
                        };
                        return user;
                    }
                }
            }
            return null;

        }

        public void postUser(UserItem userItem)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO user(user_name, user_password) VALUES(@Name, @Password)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Name", userItem.Name);
                cmd.Parameters.AddWithValue("@Password", userItem.Password);

                cmd.ExecuteNonQuery();

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
