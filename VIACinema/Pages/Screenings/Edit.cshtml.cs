using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VIACinema.Data;
using VIACinema.Models;

namespace VIACinema.Pages.Screenings
{
    public class EditModel : PageModel
    {
        private readonly VIACinema.Data.ApplicationDbContext _context;

        public EditModel(VIACinema.Data.ApplicationDbContext context)
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
           ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Screening).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreeningExists(Screening.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ScreeningExists(int id)
        {
            return _context.Screening.Any(e => e.Id == id);
        }
    }
}
