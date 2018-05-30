using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VIACinema.Data;
using VIACinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VIACinema.Pages.Tickets
{
    public class IndexModel : PageModel
    {
        private readonly VIACinema.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(VIACinema.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Ticket> Ticket { get; set; }

        public async Task OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
//         
            Ticket = await _context.Ticket.Where(t => t.User == currentUser)
                .Include(t => t.Screening).ToListAsync();
        }
    }
}
