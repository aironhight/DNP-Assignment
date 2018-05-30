using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VIACinema.Data;
using VIACinema.Models;

namespace VIACinema.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly VIACinema.Data.ApplicationDbContext _context;

        public CreateModel(VIACinema.Data.ApplicationDbContext context)
        {
            _context = context;
        }



        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}