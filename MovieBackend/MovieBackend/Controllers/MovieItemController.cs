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
        private readonly MovieItemContext _context;

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<MovieItem>> Get()
        {
            return _context.GetAllMovies();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public MovieItem Get(int id)
        {
            return _context.GetMovieWithId(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]MovieItem movie)
        {
            _context.PostNewMovie(movie);
        }

        // PUT api/<controller>/5
        // change the list name
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]float rating)
        {
            _context.UpdateMovieRating(id, rating);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.DeleteMovie(id);
        }

        public MovieItemController(MovieItemContext context)
        {
            _context = context;
        }
    }
}
