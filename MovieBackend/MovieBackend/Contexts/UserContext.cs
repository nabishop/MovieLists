using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MovieBackend.Models
{
    public class UserContext : IdentityDbContext
    {
        // context helper methods
        public string ConnectionString { get; set; }

        public DbSet<UserItem> UserItems { get; set; }

        public UserContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public UserContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        // api helper methods

        // get
        public List<UserItem> GetUsers()
        {
            List<UserItem> list = new List<UserItem>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM user";

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
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return list;

        }

        // get + id
        public UserItem GetUserWithId(int id)
        {
            UserItem user;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM user WHERE user_id=@ID";
                cmd.Parameters.AddWithValue("@ID", id);

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
                        if (conn != null)
                        {
                            conn.Close();
                        }

                        return user;
                    }
                }
            }
            return null;

        }

        // get + id
        public UserItem GetUserWithName(string name)
        {
            UserItem user;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM user WHERE user_name=@ID";
                cmd.Parameters.AddWithValue("@ID", name);

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
                        if (conn != null)
                        {
                            conn.Close();
                        }

                        return user;
                    }
                }
            }
            return null;

        }

        // post
        public void PostUser(UserItem userItem)
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


        // set a new password for a user
        public void PutNewPassword(int id, string password)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE user SET user_password=@NewPassword WHERE user_id=" + id;
                cmd.Parameters.AddWithValue("@NewPassword", password);

                cmd.ExecuteNonQuery();
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void DeleteUser(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM user where user_id=" + id;

                cmd.ExecuteNonQuery();
                conn.Close();
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }


    }
}
