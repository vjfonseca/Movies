using System;
using System.Collections.Generic;
using Movies.Models;
using System.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

using Movies.Data.Wrapper;

namespace Movies.Data
{
    class SqlMovieRepo : IMovieRepo
    {
        private readonly MovieContext _context;
        public SqlMovieRepo(MovieContext context)
        {
            this._context = context;
        }
        public Movie getById(int id)
        {
            var repo = _context.Movies.Include(x => x.Actors).FirstOrDefault(x => x.Id == id);
            return repo;
        }
        public List<Movie> getAll()
        {
            var movies = _context.Movies.ToList();
            return movies;
        }
        public void add(Movie movie)
        {
            _context.Movies.Add(movie);
            EFWrapper.mapActor(_context, movie);
        }
        public void Delete(Movie movie)
        {
            _context.Movies.Remove(this.getById(movie.Id));
        }
        public void AddActor(Actor actor)
        {
            _context.Add(actor);
        }

        public Actor GetActorById(int id)
        {
            return _context.Actors.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Actor> GetActors()
        {
            return _context.Actors.Include(x => x.Movies);
        }

        public void DeleteActor(int id)
        {
            _context.Actors.Remove(this.GetActorById(id));
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() < 1;
        }
        public Movie Replace(Movie newMovie, int id)
        {
            var old = getById(id);
            EFWrapper.replaceActors(_context, newMovie, old);
            return getById(id);
        }
        public Movie MapNested(Movie movie)
        {
            throw new NotImplementedException();
        }
    }

}