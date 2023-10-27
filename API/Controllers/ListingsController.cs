using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    /// <summary>
    /// This class performs a definition to api services to listnigs table.
    /// </summary>
    public class ListingsController : BaseApiController
    {
        private readonly DataContext _context;

        public ListingsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Listing>>> GetListinges()
        {
            return await _context.Listings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Listing>> GetListing(Guid id)
        {
            return await _context.Listings.FindAsync(id);
        }
    }
}
