using System;
using System.Collections.Generic;

namespace VIACinema.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }

  

        public ICollection<Screening> Screenings { get; set; }
    }
}