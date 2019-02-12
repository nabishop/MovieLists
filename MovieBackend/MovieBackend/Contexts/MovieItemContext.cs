using Microsoft.EntityFrameworkCore;
using MovieBackend.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MovieBackend.Contexts
{
    // general helpers
    public class MovieItemContext : DbContext
    {
        public string ConnectionString { get; set; }

        public DbSet<MovieItem> MovieItems { get; set; }

        public MovieItemContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        // helper methods for API

        // get all movies
        public List<MovieItem> GetAllMovies()
        {
            List<MovieItem> movies = new List<MovieItem>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM movie";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new MovieItem()
                        {
                            ID = Convert.ToInt32(reader["movie_id"]),
                            Title = reader["title"].ToString(),
                            Description = reader["description"].ToString(),
                            ReleaseDate = reader["release_date"].ToString(),
                            Rating = Convert.ToDouble(reader["rating"]),
                            UserID = Convert.ToInt32(reader["user_id"]),
                            ListName = reader["list_name"].ToString()
                        });
                    }
                }
                if (conn != null)
                {
                    conn.Close();
                }
                return movies;
            }
        }

        // get + id
        public List<MovieItem> GetMovieWithListName(string listname)
        {
            List<MovieItem> movies = new List<MovieItem>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM movie WHERE list_name=@Name";
                cmd.Parameters.AddWithValue("@Name", listname);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new MovieItem()
                        {
                            ID = Convert.ToInt32(reader["movie_id"]),
                            Title = reader["title"].ToString(),
                            Description = reader["description"].ToString(),
                            ReleaseDate = reader["release_date"].ToString(),
                            Rating = Convert.ToDouble(reader["rating"]),
                            UserID = Convert.ToInt32(reader["user_id"]),
                            ListName = reader["list_name"].ToString()
                        });
                    }
                }
            }
            return movies;
        }

        // add a new movie to the db
        public void PostNewMovie(MovieItem movie)
        {
            using(MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO movie(title, description, release_date, rating, user_id, list_name) VALUES(@Title, @Desc, @Date, @Rating, @UserID, @ListName)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Title", movie.Title);
                cmd.Parameters.AddWithValue("@Desc", movie.Description);
                cmd.Parameters.AddWithValue("@Date", movie.ReleaseDate);
                cmd.Parameters.AddWithValue("@Rating", movie.Rating);
                cmd.Parameters.AddWithValue("@UserID", movie.UserID);
                cmd.Parameters.AddWithValue("@ListName", movie.ListName);

                cmd.ExecuteNonQuery();

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }


        // update movie rating (PUT)
        public void UpdateMovieRating(int id, float rating)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                // change rating
                cmd.CommandText = "UPDATE movie SET rating=@NewRating WHERE movie_id=" + id;
                cmd.Parameters.AddWithValue("@NewRating", rating);
                cmd.ExecuteNonQuery();

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }


        // Delete movie
        public void DeleteMovie(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM movie where movie_id=" + id;
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
