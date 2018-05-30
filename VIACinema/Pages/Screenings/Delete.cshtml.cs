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
    public class DeleteModel : PageModel
    {
        private readonly VIACinema.Data.ApplicationDbContext _context;

        public DeleteModel(VIACinema.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Screening Screening { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Screening = await _context.Screening
                .Include(s => s.Movie).SingleOrDefaultAsync(m => m.Id == id);

            if (Screening == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Screening = await _context.Screening.FindAsync(id);

            if (Screening != null)
            {
                _context.Screening.Remove(Screening);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
