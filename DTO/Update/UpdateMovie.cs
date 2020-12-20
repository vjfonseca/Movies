using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Movies.DTO
{
    public class UpdateMovie
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public DateTime Release { get; set; }
        public ICollection<ActorId> Actors { get; set; }
    }
}