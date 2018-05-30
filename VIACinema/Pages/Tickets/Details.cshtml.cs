using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VIACinema.Data;
using VIACinema.Models;

namespace VIACinema.Pages.Tickets
{
    public class DetailsModel : PageModel
    {
        private readonly VIACinema.Data.ApplicationDbContext _context;

        public DetailsModel(VIACinema.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket = await _context.Ticket
                .Include(t => t.Screening).SingleOrDefaultAsync(m => m.Id == id);

            if (Ticket == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
