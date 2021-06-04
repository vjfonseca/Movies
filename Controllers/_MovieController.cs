
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using Movies.DTO;
using Movies.Models;

namespace Movies.Controllers
{
    [ApiController]
    [Route("api")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepoAsync _repo;
        private readonly IMapper _mapper;
        public MovieController(IMovieRepoAsync repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ActionResult<MovieRead>> Get(int id)
        {
            var model = await _repo.getById(id);
            if (model != null)
            {
                var dto = _mapper.Map<MovieRead>(model);
                return Ok(dto);
            }
            else return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MovieWrite mw)
        {
            var model = _mapper.Map<Movie>(mw);
            await _repo.add(model);
            await _repo.SaveChanges();
            var read = _mapper.Map<MovieRead>(model);
            return CreatedAtRoute(nameof(this.Get), new { Id = model.Id }, read);
        }
    }
}