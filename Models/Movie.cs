using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Movies.Models
{
    public class Movie 
    {
        [Key] 
        public int Id { get;  set; }
        [Required, MaxLength(100)] 
        public string Name { get;  set; }
        public DateTime Release { get; set; }
        public ICollection<Actor> Actors {get; set;}
    }
}