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
        [HttpGet("{id}")]
        public MovieItem Get(int id)
        {
            return context.GetMovieWithId(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]MovieItem movie)
        {
            context.PostNewMovie(movie);
        }

        // PUT api/<controller>/5
        // change the list name
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]float rating)
        {
            context.UpdateMovieRating(id, rating);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            context.DeleteMovie(id);
        }
    }
}
