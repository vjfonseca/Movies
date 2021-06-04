using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Movies.Models;

namespace Movies.Data.Wrapper
{
    public static class EFWrapper
    {
        public static string showTrack(DbContext _context)
        {
            string states = "";
            var Added = _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            var Deleted = _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
            var Modified = _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            var Detached = _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Detached);
            var unchanged = _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Unchanged);

            if (Added.Count() > 0) states += "Added " + Added.Count();
            if (Deleted.Count() > 0) states += " Deleted " + Deleted.Count();
            if (Modified.Count() > 0) states += " Modified " + Modified.Count();
            if (Detached.Count() > 0) states += " Detached " + Detached.Count();
            if (unchanged.Count() > 0) states += " unchanged " + unchanged.Count();

            if (states == "") { return states; }
            else return states;
        }
        public static string DisplayStates(IEnumerable<EntityEntry> entries)
        {
            string output = "";
            foreach (var entry in entries)
            {
                output +=
                ($"Entity:{entry.Entity.GetType().Name},State:{ entry.State.ToString()} - ");
            }
            return output;
        }
        public static void mapActor(DbContext context, Movie movie)
        {
            foreach (Actor actor in movie.Actors)
            {
                Actor repo = context.Set<Actor>().AsNoTracking().Single(x => x.Id == actor.Id);
                context.Entry(actor).CurrentValues.SetValues(repo);
                context.Entry(actor).Reload();
            }
        }
        public static void replaceActors(DbContext context, Movie source, Movie destination)
        {
            context.Entry(destination).CurrentValues.SetValues(source);
            destination.Actors.Clear();
            foreach (var actor in source.Actors)
            {
                var repo = context.Set<Actor>().Single(x => x.Id == actor.Id);
                destination.Actors.Add(repo);
            }
        }
        public static async void mapActorAsync(DbContext context, Movie movie)
        {
            foreach (Actor actor in movie.Actors)
            {
                Actor repo = await context.Set<Actor>().AsNoTracking().SingleAsync(x => x.Id == actor.Id);
                context.Entry(actor).CurrentValues.SetValues(repo);
                context.Entry(actor).Reload();
            }
        }
        public async static Task replaceActorsAsync(DbContext context, Movie source, Movie destination)
        {
            context.Entry(destination).CurrentValues.SetValues(source);
            destination.Actors.Clear();
            foreach (var actor in source.Actors)
            {
                var repo = await context.Set<Actor>().SingleAsync(x => x.Id == actor.Id);
                destination.Actors.Add(repo);
            }
        }
    }
}

