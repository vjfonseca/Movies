using Movies.Models;
using Movies.Data;
using Movies.DTO;

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.JsonPatch;

namespace Movies.Controllers
{
    [Route("Movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepo _repository;
        private readonly IMapper _mapper;
        public MoviesController(IMovieRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var repo = _repository.getById(id);
            _repository.Delete(repo);
            _repository.SaveChanges();
            return NoContent();
        }
        [HttpGet(Name = "All")]
        public ActionResult<IEnumerable<MovieRead>> All()
        {
            var movies = _repository.getAll();
            return Ok(_mapper.Map<IEnumerable<MovieRead>>(movies));
        }
        [HttpPost(Name = "Create")]
        public ActionResult Create([FromBody] MovieWrite write)
        {
            var movie = _mapper.Map<Movie>(write);
            _repository.add(movie);
            _repository.SaveChanges();
            var read = _mapper.Map<MovieRead>(movie);
            return CreatedAtRoute(nameof(Get), new { Id = read.Id }, read);
        }
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<MovieRead> Get(int id)
        {
            var movie = _repository.getById(id);
            var read = _mapper.Map<MovieRead>(movie);
            return Ok(read);
        }
        [HttpPut(Name = "Put")]
        public ActionResult<MovieRead> Put([FromBody]UpdateMovie update)
        {
            var newMovie = _mapper.Map<Movie>(update);
            _repository.Replace(newMovie);
            _repository.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id}", Name = "Patch")]
        public ActionResult Patch(int id, [FromBody] JsonPatchDocument<UpdateMovie> patchDocument)
        {
            Movie oldMovie = _repository.getById(id);
            UpdateMovie movie = _mapper.Map<UpdateMovie>(oldMovie);

            patchDocument.ApplyTo(movie);
            Movie newValues = _mapper.Map<Movie>(movie);
            _repository.Replace(newValues);
            _repository.SaveChanges();

            return NoContent();
        }
        [HttpPost("Actors", Name = "CreateActor")]
        public ActionResult CreateActor([FromBody] ActorWrite write)
        {
            var actor = _mapper.Map<Actor>(write);
            _repository.AddActor(actor);
            _repository.SaveChanges();
            var read = _repository.GetActorById(actor.Id);

            return CreatedAtRoute(nameof(GetActorById), new { Id = read.Id }, read);
        }
        [HttpGet("Actors", Name = "GetActor")]
        public ActionResult<IEnumerable<ActorRead>> GetActors()
        {
            var actors = _repository.GetActors();
            return Ok(_mapper.Map<IEnumerable<ActorRead>>(actors));
        }
        [HttpDelete("Actors/{id}", Name = "DeleteActor")]
        public ActionResult DeleteActor(int id)
        {
            if (_repository.GetActorById(id) == null) return BadRequest();
            _repository.DeleteActor(id);
            _repository.SaveChanges();
            return NoContent();
        }
        [HttpGet("Actors/{id}", Name = "GetActorById")]
        public ActionResult<ActorRead> GetActorById(int id)
        {
            var model = _repository.GetActorById(id);
            if (model == null) return BadRequest();
            var read = _mapper.Map<ActorRead>(model);
            return Ok(read);
        }
    }
}