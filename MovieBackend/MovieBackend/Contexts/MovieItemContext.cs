using Microsoft.EntityFrameworkCore;
using MovieBackend.Models;
using MySql.Data.MySqlClient;

namespace MovieBackend.Contexts
{
    public class MovieItemContext : DbContext
    {
        public string ConnectionString { get; set; }

        public DbSet<MovieItem> UserItems { get; set; }

        public MovieItemContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
