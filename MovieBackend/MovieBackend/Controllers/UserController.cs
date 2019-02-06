using Microsoft.AspNetCore.Mvc;
using MovieBackend.Models;
using System.Collections.Generic;

namespace MovieBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext context;

        public UserController(UserContext context)
        {
            this.context = context;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<IEnumerable<UserItem>> GetAllUsers()
        {
            return context.GetUsers();
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        public UserItem GetUserWithId(int id)
        {
            return context.GetUserWithId(id);
        }

        // POST: api/user
        [HttpPost]
        public void Post([FromBody] UserItem userItem)
        {
            context.PostUser(userItem);
        }

        // PUT: api/user/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string password)
        {
            context.PutNewPassword(id, password);
        }

        // DELETE: api/user/1
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            context.DeleteUser(id);
        }
    }
}
