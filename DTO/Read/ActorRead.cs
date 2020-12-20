using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Movies.Models;

namespace Movies.DTO
{
    public class ActorRead 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // public ICollection<Movie> Movies { get; set; }
    }
}