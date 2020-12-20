using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.DTO
{
    public class MovieRead
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public string Name { get; set; }
        public DateTime Release { get; set; }
        public ICollection<ActorInMovie> Actors { get; set; }
    }
}