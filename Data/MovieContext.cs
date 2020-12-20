using Microsoft.EntityFrameworkCore;
using Movies.Data.Configuration;
using Movies.Models;

namespace Movies.Data
{
    class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public MovieContext(DbContextOptions opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ActorConfiguration());
        }

    }

}