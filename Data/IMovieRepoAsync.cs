using System.Collections.Generic;
using Movies.Models;
using System;
using System.Threading.Tasks;

namespace Movies.Data
{
    public interface IMovieRepoAsync
    {
        public Task<List<Movie>> getAll();
        public Task<Movie> getById(int id);
        public Task<bool> SaveChanges();
        public Task add(Movie movie);
        public void Delete(Movie movie);

        public Task AddActor(Actor actor);
        public Task<Actor> GetActorById(int id);
        public Task<IEnumerable<Actor>> GetActors();
        public void DeleteActor(Actor actor);
        public Task Replace(Movie newMovie, int id);
    }
}