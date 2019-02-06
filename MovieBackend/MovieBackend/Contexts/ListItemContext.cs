using Microsoft.EntityFrameworkCore;
using MovieBackend.Models;
using MySql.Data.MySqlClient;

namespace MovieBackend.Contexts
{
    public class ListItemContext : DbContext
    {
        public string ConnectionString { get; set; }

        public DbSet<ListItem> UserItems { get; set; }

        public ListItemContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
