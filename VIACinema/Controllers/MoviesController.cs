using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VIACinema.Data;
using VIACinema.Models;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// This exposes functinality for the API
/// </summary>
namespace VIACinema.Controllers
{
    [Produces("application/json")]
    [Route("api/Movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Movie> GetAll()
        {
            return _context.Movie.ToList();
        }
    }
}