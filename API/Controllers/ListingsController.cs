using Application;
using Application.Core;
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
    public class ListingsController : BaseApiController
    {
        //get all listing from database
        [HttpGet]
        public async Task<IActionResult> GetListinges([FromQuery] ListingParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
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
        [Authorize(Policy = "IsListingCreator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id,Listing listing)
        {
            listing.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Listing = listing }));
        }

        //delete Listing with given id
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [HttpPost("{id}/visit")]
        public async Task<IActionResult> Visit(Guid id)
        {
            return HandleResult(await Mediator.Send(new UpdateVisitor.Command { Id = id }));
        }

    }
}
