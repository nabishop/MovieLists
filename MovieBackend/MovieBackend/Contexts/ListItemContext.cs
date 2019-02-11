using Microsoft.EntityFrameworkCore;
using MovieBackend.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MovieBackend.Contexts
{
    public class ListItemContext : DbContext
    {
        // HELPERS

        public string ConnectionString { get; set; }

        public DbSet<ListItem> ListItems { get; set; }

        public ListItemContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        // API HELPER METHODS

        // get all lists
        public List<ListItem> GetAllListsWithUserId(int userId)
        {
            List<ListItem> lists = new List<ListItem>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM movielist WHERE user_id=" + userId;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lists.Add(new ListItem()
                        {
                            Name = reader["name"].ToString(),
                            DateAdded = reader["date_added"].ToString(),
                            Movie_ID = Convert.ToInt32(reader["movie_id"]),
                            User_ID = Convert.ToInt32(reader["user_id"])
                        });
                    }
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return lists;
        }


        // get + id
        public List<MovieItem> GetMovieList(int userId, string listName)
        {
            List<MovieItem> movielist = new List<MovieItem>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM movie WHERE movie_id AND user_id IN (SELECT movie_id AND user_id FROM movielist WHERE user_id=@ID AND name=@NAME)";
                cmd.Parameters.AddWithValue("@ID", userId);
                cmd.Parameters.AddWithValue("@NAME", listName);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movielist.Add(new MovieItem()
                        {
                            ID = Convert.ToInt32(reader["movie_id"]),
                            Title = reader["title"].ToString(),
                            Description = reader["description"].ToString(),
                            ReleaseDate = reader["release_date"].ToString(),
                            Rating = Convert.ToDouble(reader["rating"]),
                            UserID = Convert.ToInt32(reader["user_id"])
                        });
                        if (conn != null)
                        {
                            conn.Close();
                        }
                    }
                }
                return movielist;
            }
        }

        // post
        public void PostList(ListItem listItem)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO movielist(name, date_added, movie_id, user_id) VALUES(@Name, @Date, @Movie, @User)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Name", listItem.Name);
                cmd.Parameters.AddWithValue("@Date", listItem.DateAdded);
                cmd.Parameters.AddWithValue("@Movie", listItem.Movie_ID);
                cmd.Parameters.AddWithValue("@User", listItem.User_ID);

                cmd.ExecuteNonQuery();

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }


        // set a new password for a user
        public void putNewListName(int id, ChangeName change)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE movielist SET list_name=@NewName WHERE user_id=@User AND name=@OldName";
                cmd.Parameters.AddWithValue("@NewName", change.NewName);
                cmd.Parameters.AddWithValue("@User", id);
                cmd.Parameters.AddWithValue("@OldName", change.OldName);

                cmd.ExecuteNonQuery();
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void deleteList(int id, string name)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "DELETE FROM movielist where user_id=@UserID AND name=@ListName";
                cmd.Parameters.AddWithValue("@UserId", id);
                cmd.Parameters.AddWithValue("@ListName", name);

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
