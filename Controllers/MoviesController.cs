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
    [Route("api/movies")]
    [ApiController]
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
        [HttpGet("", Name = "GetAll")]
        public ActionResult<IEnumerable<MovieRead>> GetAll()
        {
            var movies = _repository.getAll();
            return Ok(_mapper.Map<IEnumerable<MovieRead>>(movies));
        }
        [HttpPost("")]
        public ActionResult Create(MovieWrite write)
        {
            var movie = _mapper.Map<Movie>(write);
            _repository.add(movie);
            _repository.SaveChanges();
            var read = _mapper.Map<MovieRead>(movie);
            return CreatedAtRoute(nameof(GetById), new { Id = read.Id }, read);
        }
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<MovieRead> GetById(int id)
        {
            var movie = _repository.getById(id);
            var read = _mapper.Map<MovieRead>(movie);
            return Ok(read);
        }

        [HttpPost("actors")]
        public ActionResult<ActorRead> CreateActor(ActorWrite write)
        {
            var actor = _mapper.Map<Actor>(write);
            _repository.AddActor(actor);
            _repository.SaveChanges();
            var created = _repository.GetActorById(actor.Id);
            
            return Ok(_mapper.Map<ActorRead>(created));
        }
        [HttpGet("actors")]
        public ActionResult<IEnumerable<ActorRead>> GetActor()
        {
            var actors = _repository.GetActors();
            return Ok(_mapper.Map<IEnumerable<ActorRead>>(actors));
        }
        [HttpDelete("actors/{id}")]
        public ActionResult DeleteActor(int id)
        {
            _repository.DeleteActor(id);
            _repository.SaveChanges();
            return NoContent();
        }
        [HttpGet("actors/{id}")]
        public ActionResult<ActorRead> GetActorById(int id)
        {
            var actor = _repository.GetActorById(id);
            var read = _mapper.Map<ActorRead>(actor);
            return Ok(read);
        }
        [HttpPatch("{id}")]
        public ActionResult<MovieRead> Patch(int id, JsonPatchDocument<UpdateMovie> patchDocument)
        {
            Movie oldMovie = _repository.getById(id);
            UpdateMovie movie = _mapper.Map<UpdateMovie>(oldMovie);

            patchDocument.ApplyTo(movie);
            Movie newValues = _mapper.Map<Movie>(movie);
            _repository.Replace(newValues, oldMovie.Id);
            _repository.SaveChanges();

            var result = _mapper.Map<MovieRead>(_repository.getById(id));
            return Ok(result);
        }
        [HttpPut("{id}")]
        public ActionResult<MovieRead> Put(int id, UpdateMovie update)
        {
            // id == update.id
            var newMovie = _mapper.Map<Movie>(update);
            var movie = _repository.Replace(newMovie, id);
            _repository.SaveChanges();
            return Ok(_mapper.Map<MovieRead>(movie));
        }
    }
}