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
                            Movie_ID = Convert.ToInt32(reader["movie_id"]),
                            Title = reader["title"].ToString(),
                            Description = reader["description"].ToString(),
                            ReleaseDate = reader["release_date"].ToString(),
                            Watched = Convert.ToBoolean(reader["watched"]),
                            Rating = Convert.ToDouble(reader["rating"])
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
        public MovieItem GetMovieWithId(int id)
        {
            MovieItem movie;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM movie WHERE movie_id=@ID";
                cmd.Parameters.AddWithValue("@ID", id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movie = new MovieItem()
                        {
                            Movie_ID = Convert.ToInt32(reader["movie_id"]),
                            Title = reader["title"].ToString(),
                            Description = reader["description"].ToString(),
                            ReleaseDate = reader["release_date"].ToString(),
                            Watched = Convert.ToBoolean(reader["watched"]),
                            Rating = Convert.ToDouble(reader["rating"])
                        };
                        if (conn != null)
                        {
                            conn.Close();
                        }

                        return movie;
                    }
                }
            }
            return null;
        }

        // add a new movie to the db
        public void PostNewMovie(MovieItem movie)
        {
            using(MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO movie(title, description, release_date, watched, rating) VALUES(@Title, @Desc, @Date, @Watched, @Rating)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Title", movie.Title);
                cmd.Parameters.AddWithValue("@Desc", movie.Description);
                cmd.Parameters.AddWithValue("@Date", movie.ReleaseDate);
                cmd.Parameters.AddWithValue("@Watched", movie.Watched);
                cmd.Parameters.AddWithValue("@Rating", movie.Rating);

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

                // update that the movie has been viewed
                cmd.CommandText = "UPDATE movie SET watched=@NewWatched WHERE movie_id=" + id;
                cmd.Parameters.AddWithValue("@NewRating", 1);
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
