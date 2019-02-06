using Microsoft.AspNetCore.Mvc;
using MovieBackend.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace MovieBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserItemContext _context;

        // GET: api/User
        [HttpGet]
        public ActionResult<IEnumerable<UserItem>> GetAllUsers()
        {
            return _context.GetUsers();
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        public UserItem GetUserWithId(int id)
        {
            return _context.GetUserWithId(id);
        }

        // POST: api/user
        [HttpPost]
        public void Post(UserItem userItem)
        {
            using (MySqlConnection conn = new MySqlConnection(_context.ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO student VALUES(" + userItem.Name + ", " + userItem.Name, conn);
                cmd.ExecuteNonQuery();
            }
        }

        // PUT: api/user/5
        [HttpPut("{id}")]
        public void Put(UserItem userItem)
        {
            using (MySqlConnection conn = new MySqlConnection(_context.ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE users SET password=" + userItem.Password + " WHERE user_id=" + userItem.Id, conn);
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE: api/user/1
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(_context.ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM users where user_id=" + id, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public UserController(UserItemContext context)
        {
            _context = context;
        }
    }
}
