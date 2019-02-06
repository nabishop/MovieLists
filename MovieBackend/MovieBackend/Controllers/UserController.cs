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
        private readonly UserContext _context;

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
        public void Post([FromBody] UserItem userItem)
        {
            _context.postUser(userItem);
        }

        // PUT: api/user/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] NewPassword password)
        {
            _context.putNewPassword(id, password);
        }

        // DELETE: api/user/1
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(_context.ConnectionString))
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

        public UserController(UserContext context)
        {
            _context = context;
        }
    }
}
