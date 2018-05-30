using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VIACinema.Models
{
    public class Screening
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Room { get; set; }
        public int Seats { get; set; }

        [ForeignKey("Movie")]
        public virtual int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
