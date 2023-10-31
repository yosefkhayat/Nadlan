using Application;
using Application.Listings;
using Domain;
using MediatR;
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
        //get all listing from database
        [HttpGet]
        public async Task<ActionResult<List<Listing>>> GetListinges()
        {
            return await Mediator.Send(new List.Query());
        }
        //get listing with given id
        [HttpGet("{id}")]
        public async Task<ActionResult<Listing>> GetListing(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }
        //create new listing
        [HttpPost]
        public async Task Create(Listing listing)
        {
            await Mediator.Send(new Create.Command { Listing = listing });
        }
        //edit Listing with given id
        [HttpPut("{id}")]
        public async Task Edit(Guid id,Listing listing)
        {
            listing.Id = id;
            await Mediator.Send(new Edit.Command { Listing = listing });
        }
        //delete Listing with given id
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await Mediator.Send(new Delete.Command { Id = id });
        }
    }
}
