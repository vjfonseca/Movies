using System;
using System.Collections.Generic;
using Movies.Models;
using System.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

using Movies.Data.Wrapper;
using System.Threading.Tasks;

namespace Movies.Data
{
    class SqlMovieRepoAsync : IMovieRepoAsync
    {
        private readonly MovieContext _context;
        public SqlMovieRepoAsync(MovieContext context)
        {
            this._context = context;
        }
        public async Task<Movie> getById(int id)
        {
            var repo = await _context.Movies.Include(x => x.Actors).FirstOrDefaultAsync(x => x.Id == id);
            return repo;
        }
        public async Task<List<Movie>> getAll()
        {
            var movies = await _context.Movies.ToListAsync();
            return movies;
        }
        public async Task add(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            EFWrapper.mapActor(_context, movie);
        }
        public void Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
        }
        public async Task AddActor(Actor actor)
        {
            await _context.AddAsync(actor);
        }
        public async Task<Actor> GetActorById(int id)
        {
            return await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<Actor>> GetActors()
        {
            return await _context.Actors.Include(x => x.Movies).ToListAsync<Actor>();
        }
        public void DeleteActor(Actor actor)
        {
            _context.Actors.Remove(actor);
        }
        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() < 1;
        }

        public async Task Replace(Movie newMovie, int id)
        {
            var old = await getById(id);
            await EFWrapper.replaceActorsAsync(_context, newMovie, old);
        }
    }

}