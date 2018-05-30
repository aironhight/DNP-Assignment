using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VIACinema.Data;
using VIACinema.Models;

namespace VIACinema.Pages.Screenings
{
    public class IndexModel : PageModel
    {
        private readonly VIACinema.Data.ApplicationDbContext _context;

        public IndexModel(VIACinema.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Screening> Screening { get;set; }

        public async Task OnGetAsync()
        {
            Screening = await _context.Screening
                .Include(s => s.Movie).ToListAsync();
        }
    }
}
