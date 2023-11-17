using Application;
using Application.Listings;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Reflection.Metadata;

namespace API.Controllers
{
    /// <summary>
    /// This class performs a definition to api services to listnigs table.
    /// </summary>
    [AllowAnonymous]
    public class ListingsController : BaseApiController
    {
        //get all listing from database
        [HttpGet]
        public async Task<IActionResult> GetListinges()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        //get listing with given id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetListing(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id })); 
        }

        //create new listing
        [HttpPost]
        public async Task<IActionResult> Create(Listing listing)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Listing = listing }));
        }

        //edit Listing with given id
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id,Listing listing)
        {
            listing.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Listing = listing }));
        }

        //delete Listing with given id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
