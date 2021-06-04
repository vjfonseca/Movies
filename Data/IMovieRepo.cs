using System.Collections.Generic;
using Movies.Models;
using System;

namespace Movies.Data
{
    public interface IMovieRepo
    {
        public List<Movie> getAll();
        public Movie getById(int id);
        public bool SaveChanges();
        public void add(Movie movie);
        public void Delete(Movie movie);
        
        public void AddActor(Actor actor);
        public Actor GetActorById (int id);
        public IEnumerable<Actor> GetActors();
        public void DeleteActor(int id);
        public void Replace(Movie newMovie);
        public Movie MapNested(Movie movie);
    }
}