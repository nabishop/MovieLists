using Microsoft.AspNetCore.Mvc;
using MovieBackend.Contexts;
using MovieBackend.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieBackend.Controllers
{
    [Route("api/list")]
    [ApiController]
    public class ListItemController : ControllerBase
    {
        private readonly ListItemContext context;

        public ListItemController(ListItemContext context)
        {
            this.context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<ListItem>> Get()
        {
            return null;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ListItem>> Get(int id)
        {
            return context.GetAllListsWithUserId(id);
        }

        // GET api/<controller>/5
        [HttpGet("{id}/{name}")]
        public ActionResult<IEnumerable<MovieItem>> Get(int userId, string name)
        {
            return context.GetMovieList(userId, name);
        }


        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]ListItem listItem)
        {
            context.PostList(listItem);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int userId, ChangeName changeName)
        {
            context.putNewListName(userId, changeName);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}/{name}")]
        public void Delete(int id, string name)
        {
            context.deleteList(id, name);
        }
    }
}
