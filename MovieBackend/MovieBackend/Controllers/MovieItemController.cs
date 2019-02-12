using Microsoft.AspNetCore.Mvc;
using MovieBackend.Contexts;
using MovieBackend.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieBackend.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieItemController : ControllerBase
    {
        private readonly MovieItemContext context;

        public MovieItemController(MovieItemContext context)
        {
            this.context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<MovieItem>> Get()
        {
            return context.GetAllMovies();
        }

        // GET api/<controller>/5
        [HttpGet("{listname}")]
        public List<MovieItem> Get(string listName)
        {
            return context.GetMovieWithListName(listName);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]MovieItem movie)
        {
            context.PostNewMovie(movie);
        }

        // PUT api/<controller>/5
        // change the list name
        [HttpPut]
        public void Put([FromBody] RatingChange ratingChange)
        {
            context.UpdateMovieRating(ratingChange.name, ratingChange.rating);
        }

        // PUT api/<controller>/5
        // change the list name
        [HttpPut("{oldname}/{newname}")]
        public void Put(string oldname, string newname)
        {
            context.UpdateMovieListName(oldname, newname);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            context.DeleteMovie(id);
        }
    }
}
