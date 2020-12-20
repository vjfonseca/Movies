using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
    
}