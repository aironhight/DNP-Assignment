using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VIACinema.Data;
using VIACinema.Models;
using Microsoft.AspNetCore.Identity;

namespace VIACinema.Pages.Tickets
{
    public class CreateModel : PageModel
    {
        private readonly VIACinema.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(VIACinema.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
        ViewData["ScreeningId"] = new SelectList(_context.Screening, "Id", "Id");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Contains(Ticket))
            {
                return RedirectToPage("./Taken");
            }

            Ticket.User = await _userManager.GetUserAsync(User);

            _context.Ticket.Add(Ticket);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private Boolean Contains(Ticket ticket)
        {
            List<Ticket> list = _context.Ticket.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (ticket.Seat == list.ElementAt(i).Seat && ticket.ScreeningId == list.ElementAt(i).ScreeningId)
                    return true;
            }
            return false;
        }
    }
}