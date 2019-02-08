using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieBackend.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MovieBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext context;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public UserController(UserContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
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

        // GET: api/user/5
        [HttpGet("{search}/{name}")]
        public UserItem GetUserWithName(string search, string name)
        {
            return context.GetUserWithName(name);
        }

        // POST: api/user
        [HttpPost]
        public void PostAsync(UserTempHolder userItem)
        {
            UserItem item = new UserItem();
            item.Name = userItem.UserName;
            item.Password = userItem.Password;

            context.PostUser(item);
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
