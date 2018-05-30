using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VIACinema.Data;

namespace VIACinema.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Seat { get; set; }

        [ForeignKey("Screening")]
        public virtual int ScreeningId { get; set; }
        public virtual Screening Screening { get; set; }

        public ApplicationUser User { get; set; }
    }
}
